import os
import requests
import io
import base64
from flask import Flask, request, send_file, render_template
from dotenv import load_dotenv
from pathlib import Path

# Инициализация Flask приложения
app = Flask(__name__)

# Этот блок кода безопасно находит и загружает .env файл
# Он будет работать и на Render, и при локальном тестировании
dotenv_path = Path(app.root_path) / '.env'
load_dotenv(dotenv_path=dotenv_path)


def call_qwen_api(image_bytes, prompt):
    """
    Функция для вызова API Alibaba DashScope (Qwen).
    """
    API_URL = "https://dashscope-intl.aliyuncs.com/api/v1/services/aigc/multimodal-generation/generation"
    API_KEY = os.getenv("QWEN_API_KEY")

    if not API_KEY:
        raise Exception("API-ключ QWEN_API_KEY не найден. Проверьте переменные окружения на Render.")

    # Кодируем изображение в Base64, как требует API
    base64_image = base64.b64encode(image_bytes).decode('utf-8')
    formatted_base64 = f"data:image/png;base64,{base64_image}"

    # Формируем тело запроса по документации
    payload = {
        "model": "qwen-image-edit",
        "input": {
            "messages": [
                {
                    "role": "user",
                    "content": [
                        {"image": formatted_base64},
                        {"text": prompt}
                    ]
                }
            ]
        },
        "parameters": {
            "watermark": False
        }
    }

    headers = {
        "Content-Type": "application/json",
        "Authorization": f"Bearer {API_KEY}"
    }

    # Отправляем запрос в API
    response = requests.post(API_URL, headers=headers, json=payload, timeout=30) # Добавлен таймаут 30 секунд

    # Проверяем ответ
    if response.status_code == 200:
        response_data = response.json()

        # Проверяем, есть ли ошибка в ответе API
        if 'output' not in response_data or 'choices' not in response_data['output']:
            raise Exception(f"Неожиданный ответ от API: {response_data}")

        image_url = response_data['output']['choices'][0]['message']['content'][0]['image']

        # Скачиваем картинку по временной ссылке
        image_response = requests.get(image_url)
        if image_response.status_code == 200:
            return image_response.content # Возвращаем байты скачанной картинки
        else:
            raise Exception(f"Не удалось скачать сгенерированное изображение. Статус: {image_response.status_code}")
    else:
        raise Exception(f"Ошибка API. Статус: {response.status_code}, Ответ: {response.text}")

# Главная страница, которая отдает наш index.html
@app.route('/')
def index():
    return render_template('index.html')

# Эндпоинт, который принимает фото и запускает генерацию
@app.route('/generate', methods=['POST'])
def generate():
    if 'image' not in request.files:
        return "Не найден файл изображения", 400

    image_file = request.files['image']
    image_bytes = image_file.read()

    # Промпт, который зашит в коде
    prompt = "Нарисуй на заднем фоне сказочный лес, людей не меняй"

    try:
        generated_image_bytes = call_qwen_api(image_bytes, prompt)
        return send_file(
            io.BytesIO(generated_image_bytes),
            mimetype='image/png'
        )
    except Exception as e:
        # Эта строка выведет детальную ошибку в лог Render
        print(f"Произошла ошибка при вызове API: {e}")
        return str(e), 500