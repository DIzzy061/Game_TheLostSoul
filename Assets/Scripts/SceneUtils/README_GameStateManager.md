# Система управления состоянием игры (GameStateManager)

## Описание
Эта система обеспечивает правильный сброс состояния игры при использовании кнопок "Вернуться в меню" и "Restart". Она автоматически уничтожает все объекты с `DontDestroyOnLoad` и сбрасывает статические данные.

## Компоненты системы

### 1. GameStateManager
Основной менеджер состояния игры. Автоматически создается и управляет жизненным циклом всех persistent объектов.

**Методы:**
- `ResetToMainMenu()` - полный сброс состояния и возврат в главное меню
- `RestartLevel()` - сброс уровня с перезапуском текущей сцены
- `ResetGameState()` - сброс только статических данных
- `DestroyPersistentObjects()` - уничтожение всех DontDestroyOnLoad объектов

### 2. GameStateManagerInitializer
Автоматически создает GameStateManager в главном меню, если он не существует.

### 3. GameStateManagerCleanup
Очищает GameStateManager при возврате в главное меню.

### 4. MenuButtonHandler
Универсальный обработчик кнопок для главного меню и других UI элементов.

## Настройка

### Для кнопок в главном меню:
1. Добавьте компонент `MenuButtonHandler` к кнопке
2. Отметьте соответствующий тип кнопки (isPlayButton, isRestartButton, etc.)

### Для экрана смерти:
Система уже интегрирована в `DeathScreen.cs`. Кнопки "Restart" и "Вернуться в меню" автоматически используют GameStateManager.

### Для других кнопок:
```csharp
// Пример использования в любом скрипте
if (GameStateManager.Instance != null)
{
    GameStateManager.Instance.ResetToMainMenu(); // Вернуться в меню
    // или
    GameStateManager.Instance.RestartLevel(); // Перезапустить уровень
    // или
    GameStateManager.Instance.ResetCameraState(); // Сбросить только камеру
}
```

## Объекты, которые автоматически сбрасываются:
- DeathScreen (экран смерти)
- PlayerPersistence (объект сохранения игрока)
- InventoryUI (интерфейс инвентаря)
- Quiver (колчан со стрелами)
- GameState (статические данные)
- CameraFollow (состояние камеры, включая peek offset)
- MainCamera (принудительно уничтожается для полного сброса)

## Логирование
Система выводит подробные логи в консоль Unity для отслеживания процесса сброса состояния.

## Требования
- Unity 2020.3 или новее
- Все сцены должны быть добавлены в Build Settings
- Главное меню должно называться "Main_Menu" 