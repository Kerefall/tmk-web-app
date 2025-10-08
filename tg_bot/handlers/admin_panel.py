import json
import logging
from aiogram import Router, F, types
from aiogram.filters import Command
from aiogram.fsm.context import FSMContext
from aiogram.fsm.state import State, StatesGroup
from aiogram.types import InlineKeyboardMarkup, InlineKeyboardButton
from aiogram.types import (Message, CallbackQuery)

from tg_bot.business_logic import file_parser
"""from business_logic.api_client import APIClient""" # Сделай обращение к апишке своей, все остальное работает исправно, парсер файлов наверное тоже на шарпах должен быть(я не ебу)

from tg_bot.config_data import config

logger = logging.getLogger(__name__)
router = Router()

ADMINS = [867597472, 1053602730]  # сюда вписываем админов(Сотрудников)

class AdminStates(StatesGroup):
    waiting_for_file = State()
    waiting_for_confirmation = State()

# Типы моделей с их отображаемыми названиями
MODEL_TYPES = {
    'nomenclature': 'Номенклатура',
    'prices': 'Цены', 
    'remnants': 'Остатки',
    'stocks': 'Склады',
    'types': 'Типы'
}

def admin_only(handler):
    async def wrapper(message: types.Message, state: FSMContext):
        if message.from_user.id not in ADMINS:
            await message.answer("Неизвестная команда")
            return
        await handler(message, state)
    return wrapper

@router.message(Command('updatedata'))
@admin_only
async def cmd_updatedata(message: types.Message, state: FSMContext):
    """
    Обработчик команды /updatedata
    Отправляет сообщение с инлайн кнопками для выбора типа модели
    """
    # Очищаем предыдущее состояние
    await state.clear()
    
    # Создаем инлайн клавиатуру с типами моделей
    keyboard = InlineKeyboardMarkup(
        inline_keyboard=[
            [InlineKeyboardButton(
                text=display_name,
                callback_data=f"model_type:{model_key}"
            )]
            for model_key, display_name in MODEL_TYPES.items()
        ]
    )
    
    await message.answer(
        "🔧 <b>Панель администратора</b>\n\n"
        "Выберите тип модели для обновления данных:",
        reply_markup=keyboard,
        parse_mode="HTML"
    )

@router.callback_query(F.data.startswith('model_type:'))
async def process_model_selection(callback: CallbackQuery, state: FSMContext):
    """
    Обработчик выбора типа модели через callback
    Изменяет сообщение и переводит в ожидание файла
    """
    # Извлекаем тип модели из callback_data
    model_type = callback.data.split(':', 1)[1]
    
    # Проверяем валидность типа модели
    if model_type not in MODEL_TYPES:
        await callback.answer("❌ Неизвестный тип модели", show_alert=True)
        return
    
    # Сохраняем выбранный тип модели в состоянии
    await state.update_data(selected_model_type=model_type)
    await state.set_state(AdminStates.waiting_for_file)
    
    # Получаем отображаемое название типа модели
    model_display_name = MODEL_TYPES[model_type]
    
    # Изменяем сообщение, убираем кнопки
    await callback.message.edit_text(
        f"📁 <b>Загрузка данных</b>\n\n"
        f"Отправьте <b>{model_display_name}</b> в виде файла формата JSON\n\n"
        f"📋 <i>Требования к файлу:</i>\n"
        f"• Формат: .json\n"
        f"• Кодировка: UTF-8",
        parse_mode="HTML"
    )
    
    await callback.answer()

@router.message(AdminStates.waiting_for_file, F.document)
async def process_uploaded_file(message: types.Message, state: FSMContext):
    """
    Обработчик загруженного файла
    Валидирует файл и запрашивает подтверждение
    """
    document = message.document
    user_data = await state.get_data()
    model_type = user_data.get('selected_model_type')
    
    # Проверяем, что тип модели сохранен
    if not model_type:
        await message.answer("❌ Ошибка: тип модели не определен. Начните заново с /updatedata")
        await state.clear()
        return
    
    try:      
        # Валидация расширения файла
        if not document.file_name or not document.file_name.lower().endswith('.json'):
            await message.answer(
                "❌ <b>Неверный формат файла</b>\n\n"
                "Пожалуйста, отправьте файл с расширением .json",
                parse_mode="HTML"
            )
            return
        
        # Скачиваем файл
        processing_msg = await message.answer("⏳ Обработка файла...")
        
        file_info = await message.bot.get_file(document.file_id)
        file_content = await message.bot.download_file(file_info.file_path)
        
        # Парсим и валидируем файл
        file_parser = FileParser()
        parsed_data = await file_parser.parse_and_validate(
            file_content=file_content,
            model_type=model_type,
            filename=document.file_name
        )
        
        # Удаляем сообщение об обработке
        await processing_msg.delete()
        
        # Сохраняем данные для подтверждения
        await state.update_data(
            parsed_data=parsed_data,
            filename=document.file_name,
            file_size=document.file_size  # Это можно оставить для инфы юзеру
        )
        await state.set_state(AdminStates.waiting_for_confirmation)
        
        # Отправляем сообщение с информацией о файле
        model_display_name = MODEL_TYPES[model_type]
        records_count = len(parsed_data) if isinstance(parsed_data, list) else "N/A"
        
        confirmation_keyboard = InlineKeyboardMarkup(
            inline_keyboard=[
                [
                    InlineKeyboardButton(text="✅ Да, загрузить", callback_data="confirm_upload:yes"),
                    InlineKeyboardButton(text="❌ Отменить", callback_data="confirm_upload:no")
                ]
            ]
        )
        
        await message.answer(
            f"📊 <b>Проверка данных</b>\n\n"
            f"<b>Тип данных:</b> {model_display_name}\n"
            f"<b>Имя файла:</b> {document.file_name}\n"
            f"<b>Размер:</b> {document.file_size / 1024:.1f} KB\n"
            f"<b>Количество записей:</b> {records_count}\n\n"
            f"❓ Подтвердите загрузку данных в систему:",
            reply_markup=confirmation_keyboard,
            parse_mode="HTML"
        )
        
    except json.JSONDecodeError as e:
        await message.answer(
            f"❌ <b>Ошибка парсинга JSON</b>\n\n"
            f"Файл содержит некорректный JSON:\n"
            f"<code>{str(e)}</code>\n\n"
            f"Пожалуйста, проверьте формат файла и отправьте его повторно.",
            parse_mode="HTML"
        )
    except ValueError as e:
        await message.answer(
            f"❌ <b>Ошибка валидации данных</b>\n\n"
            f"<code>{str(e)}</code>\n\n"
            f"Пожалуйста, исправьте данные и отправьте файл повторно.",
            parse_mode="HTML"
        )
    except Exception as e:
        logger.error(f"Unexpected error processing file: {e}")
        await message.answer(
            f"❌ <b>Неожиданная ошибка</b>\n\n"
            f"Произошла ошибка при обработке файла:\n"
            f"<code>{str(e)}</code>\n\n"
            f"Попробуйте отправить файл повторно или обратитесь к администратору.",
            parse_mode="HTML"
        )

@router.callback_query(F.data.startswith('confirm_upload:'), AdminStates.waiting_for_confirmation)
async def process_upload_confirmation(callback: CallbackQuery, state: FSMContext):
    """
    Обработчик подтверждения загрузки данных
    """
    confirmation = callback.data.split(':', 1)[1]
    user_data = await state.get_data()
    
    if confirmation == "yes":
        try:
            # Отправляем данные через API
            processing_msg = await callback.message.edit_text(
                "⏳ <b>Отправка данных в систему...</b>\n\nПожалуйста, подождите.",
                parse_mode="HTML"
            )
            
            api_client = APIClient()
            result = await api_client.send_data(
                model_type=user_data['selected_model_type'],
                data=user_data['parsed_data']
            )
            
            # Успешная отправка
            model_display_name = MODEL_TYPES[user_data['selected_model_type']]
            await processing_msg.edit_text(
                f"✅ <b>Данные успешно обновлены!</b>\n\n"
                f"<b>Тип:</b> {model_display_name}\n"
                f"<b>Файл:</b> {user_data['filename']}\n"
                f"<b>Статус:</b> {result.get('status', 'OK')}\n"
                f"<b>Записей обработано:</b> {result.get('processed_count', 'N/A')}",
                parse_mode="HTML"
            )
            
            await state.clear()
            
        except Exception as e:
            logger.error(f"API error: {e}")
            await callback.message.edit_text(
                f"❌ <b>Ошибка отправки данных</b>\n\n"
                f"Не удалось отправить данные в систему:\n"
                f"<code>{str(e)}</code>\n\n"
                f"Попробуйте повторить операцию позже или обратитесь к администратору.",
                parse_mode="HTML"
            )
            await state.clear()
    
    elif confirmation == "no":
        await callback.message.edit_text(
            "❌ <b>Операция отменена</b>\n\n"
            "Загрузка данных была отменена.\n"
            "Для повторной попытки используйте команду /updatedata",
            parse_mode="HTML"
        )
        await state.clear()
    
    await callback.answer()

@router.message(AdminStates.waiting_for_file)
async def handle_invalid_file_input(message: types.Message):
    """
    Обработчик неправильного ввода при ожидании файла
    """
    await message.answer(
        "❌ <b>Неверный тип сообщения</b>\n\n"
        "Пожалуйста, отправьте файл в формате JSON.\n\n"
        "Для отмены операции используйте /start",
        parse_mode="HTML"
    )

@router.message(AdminStates.waiting_for_confirmation)
async def handle_invalid_confirmation_input(message: types.Message):
    """
    Обработчик неправильного ввода при ожидании подтверждения
    """
    await message.answer(
        "❓ <b>Ожидается подтверждение</b>\n\n"
        "Пожалуйста, используйте кнопки выше для подтверждения или отмены загрузки данных.",
        parse_mode="HTML"
    )