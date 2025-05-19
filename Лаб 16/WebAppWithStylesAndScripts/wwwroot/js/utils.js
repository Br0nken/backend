// Форматирование даты
function formatDate(date) {
    if (!date) return '';

    const options = {
        year: 'numeric',
        month: 'long',
        day: 'numeric',
        hour: '2-digit',
        minute: '2-digit'
    };

    return new Date(date).toLocaleDateString('ru-RU', options);
}

// Проверка email
function isValidEmail(email) {
    const re = /^[^\s@]+@[^\s@]+\.[^\s@]+$/;
    return re.test(email);
}

// Экспортируем функции
export { formatDate, isValidEmail };