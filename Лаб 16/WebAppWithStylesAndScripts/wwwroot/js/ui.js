// Функция для плавного скролла
function smoothScrollTo(target) {
    document.querySelector(target).scrollIntoView({
        behavior: 'smooth'
    });
}

// Инициализация кнопок с атрибутом data-scroll-to
function initScrollButtons() {
    const buttons = document.querySelectorAll('[data-scroll-to]');

    buttons.forEach(btn => {
        btn.addEventListener('click', function () {
            const target = this.getAttribute('data-scroll-to');
            smoothScrollTo(target);
        });
    });
}

// Добавляем в глобальную область видимости
window.initScrollButtons = initScrollButtons;