using System;

namespace PracticMaps.Exceptions
{
    /// <summary>
    /// Базовый класс исключений при работе с файлами приложения
    /// </summary>
    class PmException_CommonIOError : Exception
    {
        public readonly string Path;
        /// <summary>
        /// Базовое исключение ввода-вывода при доступе к данным
        /// </summary>
        /// <param name="message">Описание причины исключения</param>
        /// <param name="path">Путь к источнику данных</param>
        /// <param name="exception">Исключение, изначально возникшее при доступе к данным</param>
        public PmException_CommonIOError(string message, string path, Exception exception) : base(message, exception)
        {
            Path = path;
        }
        /// <summary>
        /// Базовое исключение ввода-вывода при доступе к данным
        /// </summary>
        /// <param name="message">Описание причины исключения</param>
        /// <param name="path">Путь к источнику данных</param>
        public PmException_CommonIOError(string message, string path) : base(message)
        {
            Path = path;
        }
    }

    /// <summary>
    /// Класс исключений, выбрасываемых при отсутствии корректных данных в источнике
    /// </summary>
    class PmException_DataSourceIsEmpty : PmException_CommonIOError
    {
        /// <summary>
        /// Исключение, выбрасываемое при отсутствии корректных данных в источнике
        /// </summary>
        /// <param name="message">Описание причины исключения</param>
        /// <param name="path">Путь к источнику данных</param>
        public PmException_DataSourceIsEmpty(string message, string path) : base(message, path)
        {
        }
    }

    /// <summary>
    /// Класс исключений, выбрасываемых при частичной некорректности данных в источнике
    /// </summary>
    class PmException_DataIsPartiallyIncorrect : PmException_CommonIOError
    {
        /// <summary>
        /// Исключение, выбрасываемое при частичной некорректности данных в источнике
        /// </summary>
        /// <param name="message">Описание причины исключения</param>
        /// <param name="path">Путь к источнику данных</param>
        /// <param name="exception">Исключение, изначально возникшее при доступе к данным</param>
        public PmException_DataIsPartiallyIncorrect(string message, string path, Exception exception) : base(message, path, exception)
        {
        }
        /// <summary>
        /// Исключение, выбрасываемое при частичной некорректности данных в источнике
        /// </summary>
        /// <param name="message">Описание причины исключения</param>
        /// <param name="path">Путь к источнику данных</param>
        public PmException_DataIsPartiallyIncorrect(string message, string path) : base(message, path)
        {
        }
    }
    /// <summary>
    /// Класс исключений, выбрасываемых при невозможности доступа к источнику данных
    /// </summary>
    class PmException_DataSourceAccessError : PmException_CommonIOError
    {
        /// <summary>
        /// Исключение, выбрасываемое при невозможности доступа к источнику данных
        /// </summary>
        /// <param name="message">Описание причины исключения</param>
        /// <param name="path">Путь к источнику данных</param>
        /// <param name="exception">Исключение, изначально возникшее при доступе к данным</param>
        public PmException_DataSourceAccessError(string message, string path, Exception exception) : base(message, path, exception)
        {
        }
        /// <summary>
        /// Исключение, выбрасываемое при невозможности доступа к источнику данных
        /// </summary>
        /// <param name="message">Описание причины исключения</param>
        /// <param name="path">Путь к источнику данных</param>
        public PmException_DataSourceAccessError(string message, string path) : base(message, path)
        {
        }
    }

    /// <summary>
    /// Класс исключений, выбрасываемых при отсутствии источника данных на диске
    /// </summary>
    class PmException_DataSourceIsNotExists : PmException_CommonIOError
    {
        /// <summary>
        /// Исключение, выбрасываемое при отсутствии источника данных на диске
        /// </summary>
        /// <param name="message">Описание причины исключения</param>
        /// <param name="path">Путь к источнику данных c данными</param>
        public PmException_DataSourceIsNotExists(string message, string path) : base(message, path)
        {
        }
        /// <summary>
        /// Исключение, выбрасываемое при отсутствии источника данных на диске
        /// </summary>
        /// <param name="message">Описание причины исключения</param>
        /// <param name="path">Путь к источнику данных c данными</param>
        /// <param name="exception">Исключение, изначально возникшее при доступе к файлу</param>
        public PmException_DataSourceIsNotExists(string message, string path, Exception exception) : base(message, path, exception)
        {
        }
    }
    /// <summary>
    /// Класс исключений, выбрасываемых при работе внешних утилит
    /// </summary>
    class PmException_ExternalUtilityError : Exception
    {
        /// <summary>
        /// Исключение, выбрасываемое при сбое работы внешней утилиты
        /// </summary>
        /// <param name="message"></param>
        public PmException_ExternalUtilityError(string message) : base(message)
        {

        }
        /// <summary>
        /// Исключение, выбрасываемое при сбое работы внешней утилиты
        /// </summary>
        /// <param name="message"></param>
        /// <param name="exception">Исключение, изначально возникшее при работе внешней утилиты</param>
        public PmException_ExternalUtilityError(string message, Exception ex) : base(message, ex)
        {

        }
    }

    /// <summary>
    /// Базовый класс исключений при работе с данными проекта
    /// </summary>
    class PmException_CommonDataError : Exception
    {
        /// <summary>
        /// Базовое исключение, выбрасываемое при ошибках работы с данными проекта
        /// </summary>
        /// <param name="message">Описание причины исключения</param>
        /// <param name="exception">Исключение, изначально возникшее при доступе к данным</param>
        public PmException_CommonDataError(string message, Exception exception) : base(message, exception)
        {

        }
        /// <summary>
        /// Базовое исключение, выбрасываемое при ошибках работы с данными проекта
        /// </summary>
        /// <param name="message">Описание причины исключения</param>
        public PmException_CommonDataError(string message) : base(message)
        {

        }
    }

    /// <summary>
    /// Класс исключений, выбрасываемых при невозможности обработки данного типа объекта 
    /// </summary>
    class PmException_NotSupportedObjectType : PmException_CommonDataError
    {
        /// <summary>
        /// Исключение, выбрасываемое при невозможности обработки данного типа объекта 
        /// </summary>
        /// <param name="message"></param>
        public PmException_NotSupportedObjectType(string message) : base(message)
        { 
        }
    }

    /// <summary>
    /// Класс исключений, выбрасываемых при невозможности найти родительский объект
    /// </summary>
    class PmException_ParentObjectNotFound : PmException_CommonDataError
    {
        /// <summary>
        /// Исключение, выбрасываемое при невозможности найти родительский объект
        /// </summary>
        /// <param name="message"></param>
        public PmException_ParentObjectNotFound(string message) : base(message)
        {
        }
    }

    /// <summary>
    /// Класс исключений, выбрасываемых при невозможности найти объект
    /// </summary>
    class PmException_ObjectNotFound : PmException_CommonDataError
    {
        /// <summary>
        /// Исключение, выбрасываемое при невозможности найти объект
        /// </summary>
        /// <param name="message"></param>
        public PmException_ObjectNotFound(string message) : base(message)
        {
        }
    }


    /// <summary>
    /// Класс исключений при попытке удаления последнего элемента данных
    /// </summary>
    class PmException_EmptyParentAfterDeletion : PmException_CommonDataError
    {
        /// <summary>
        /// Исключение, выбрасываемое при попытке удаления последнего дочернего элемента 
        /// </summary>
        /// <param name="message">Описание причины исключения</param>
        /// <param name="exception">Исключение, изначально возникшее при доступе к данным</param>
        public PmException_EmptyParentAfterDeletion(string message, Exception exception) : base(message, exception)
        {

        }
        /// <summary>
        /// Исключение, выбрасываемое при попытке удаления последнего дочернего элемента
        /// </summary>
        /// <param name="message">Описание причины исключения</param>
        public PmException_EmptyParentAfterDeletion(string message) : base(message)
        {

        }
    }
}
