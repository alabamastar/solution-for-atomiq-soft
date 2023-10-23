# Решение тестового задания для Aтомик Софт
Проект реализован на связке из .NET 6.0 и Avalonia 0.10.18 в качестве кроссплатформенного UI-фреймворка. 

## Особенности реализации
В рамках решения был реализован следующий функционал:
1) добавление книги из дерева в список;
2) добавление всех книг, относящихся к жанру, из дерева в список;
3) поиск по книгам внутри дерева;
4) сворачивание/разворачивание дерева;
5) удаление книг из списка.

## Описание функциональных компонент

1 - строка для ввода поискового запроса.\
2 - кнопка поиска.\
3 - свернуть все узлы дерева.\
4 - развернуть все узлы дерева.\
5 - удалить строку.

## Сборка
Команды для сборки:
```
# Linux build:
dotnet publish -c release -r linux-x64

# Windows build:
dotnet publish -c release -r win-x64
```

Готовые сборки, предоставленные в архиве, тестировались на следующих системах:\
Linux build: Ubuntu 22.04.3\
Windows build: Windows 10 (22H2)