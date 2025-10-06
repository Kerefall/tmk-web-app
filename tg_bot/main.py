import asyncio

from aiogram import Bot, Dispatcher
from config_data.config import Config, load_config
from handlers import first_handlers


# Функция конфига и запуска бота
async def main():

    # загружаем конфиг
    config: Config = load_config()

    # инициализируем бота и диспетчер
    bot = Bot(token=config.tg_bot.token)
    dp = Dispatcher()

    # Регистрируем роутеры в диспетчере
    dp.include_router(first_handlers.router)


    # Пропускаем накопившиеся апдейты и запускаем пуллинг
    await bot.delete_webhook(drop_pending_updates=False)
    await dp.start_polling(bot, allowed_updates=[])


asyncio.run(main())
