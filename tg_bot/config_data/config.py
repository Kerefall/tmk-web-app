from dataclasses import dataclass
from environs import Env

BOT_TOKEN = "8202233683:AAGzYhiw_S6cmmp3V0cdfFz8XuDBe9AnZOM"
@dataclass
class TgBot:
    token: str  # API токен бота

@dataclass
class Config:
    tg_bot: TgBot

def load_config(path: str | None = None) -> Config:
    env = Env()
    env.read_env(path)
    return Config(tg_bot=TgBot(token=env('BOT_TOKEN')))

config = load_config(".env")