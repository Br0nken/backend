// Основная функция инициализации
function init() {
    console.log('Сайт инициализирован');
    setupTooltips();
}

// Показываем tooltip при наведении на элементы с атрибутом data-tooltip
function setupTooltips() {
    const elements = document.querySelectorAll('[data-tooltip]');

    elements.forEach(el => {
        el.addEventListener('mouseenter', showTooltip);
        el.addEventListener('mouseleave', hideTooltip);
    });
}

function showTooltip(e) {
    const tooltipText = this.getAttribute('data-tooltip');
    const tooltip = document.createElement('div');
    tooltip.className = 'tooltip';
    tooltip.textContent = tooltipText;
    document.body.appendChild(tooltip);

    const rect = this.getBoundingClientRect();
    tooltip.style.top = `${rect.top - tooltip.offsetHeight - 5}px`;
    tooltip.style.left = `${rect.left + rect.width / 2 - tooltip.offsetWidth / 2}px`;
}

function hideTooltip() {
    const tooltip = document.querySelector('.tooltip');
    if (tooltip) {
        tooltip.remove();
    }
}

// Запускаем инициализацию после загрузки DOM
document.addEventListener('DOMContentLoaded', init);