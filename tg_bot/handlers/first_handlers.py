from aiogram import Router
from aiogram.filters import Command, CommandStart
from aiogram.types import (Message, InlineKeyboardMarkup,
                           InlineKeyboardButton, CallbackQuery)


router = Router()


@router.message(CommandStart())
async def command_start(message: Message):
    open_webapp_button = InlineKeyboardButton(
        text = 'Открыть магазин',
        callback_data = 'open_webapp'
    )
    keyboard: list[list[InlineKeyboardButton]] = [
        [open_webapp_button]
    ]
    markup = InlineKeyboardMarkup(inline_keyboard=keyboard)
    await message.answer("Привет! Я бот для веб-магазина от ТМК\n"
                         "Нажимай на кнопку, если хочешь открыть магазин",
                         reply_markup=markup)