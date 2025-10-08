import json

class FileParser:
    def __init__(self):
        # mapping: model_type -> ключ (начало) + валидатор
        self.model_mapping = {
            "prices": {"prefix": "ArrayOfPrices", "validator": self._validate_prices},
            "types": {"prefix": "ArrayOfType", "validator": self._validate_types},
            "stocks": {"prefix": "ArrayOfStock", "validator": self._validate_stocks},
            "nomenclature": {"prefix": "ArrayOfNomenclature", "validator": self._validate_nomenclature}
        }

    async def parse_and_validate(self, file_content, model_type, filename=None):
        try:
            text = file_content.read().decode("utf-8")
            raw_data = json.loads(text)
        except Exception as e:
            raise ValueError(f"Ошибка чтения/декодирования: {e}")

        # Получаем параметры для выбранной модели
        mapping = self.model_mapping.get(model_type)
        if not mapping:
            raise ValueError("Неизвестный тип модели")
        prefix = mapping["prefix"]

        # Ищем нужный ключ в json, начинающийся с prefix
        keys = [k for k in raw_data.keys() if k.startswith(prefix)]
        if not keys:
            raise ValueError(f"Не найден ключ, начинающийся на \"{prefix}\" в JSON файле!")
        key = keys[0]
        items = raw_data[key]

        if not isinstance(items, list):
            raise ValueError(f"Ключ \"{key}\" должен быть массивом объектов!")

        # Валидируем содержимое
        mapping["validator"](items)
        return items

    def _validate_prices(self, items):
        required = ["ID", "IDStock", "PriceT"]
        for el in items:
            for key in required:
                if key not in el:
                    raise ValueError(f"В объекте price отсутствует поле: {key}")

    def _validate_types(self, items):
        required = ["IDType", "Type"]
        for el in items:
            for key in required:
                if key not in el:
                    raise ValueError(f"В объекте type отсутствует поле: {key}")

    def _validate_stocks(self, items):
        required = ["IDStock", "Stock", "Address"]
        for el in items:
            for key in required:
                if key not in el:
                    raise ValueError(f"В объекте stock отсутствует поле: {key}")

    def _validate_nomenclature(self, items):
        required = ["ID", "Name", "IDType"]
        for el in items:
            for key in required:
                if key not in el:
                    raise ValueError(f"В объекте nomenclature отсутствует поле: {key}")