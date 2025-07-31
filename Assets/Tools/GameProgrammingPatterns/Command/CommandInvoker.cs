using System.Collections;
using System.Collections.Generic;

namespace Tools.GameProgrammingPatterns.Command
{
    /// <summary>
    /// 命令发出者
    /// </summary>
    public abstract class CommandInvoker
    {
        /// <summary>
        /// 要撤销的命令栈
        /// </summary>
        protected Stack<ICommand> _undoStack = new Stack<ICommand>();

        /// <summary>
        /// 要重做的命令栈
        /// </summary>
        protected Stack<ICommand> _redoStack = new Stack<ICommand>();

        /// <summary>
        /// 执行命令
        /// </summary>
        /// <param name="command">要执行的命令</param>
        public void ExecuteCommand(ICommand command)
        {
            command.Execute();
            _undoStack.Push(command);

            _redoStack.Clear();
        }

        /// <summary>
        /// 撤销命令
        /// </summary>
        public void UndoCommand()
        {
            if (_undoStack.Count > 0)
            {
                ICommand activeCommand = _undoStack.Pop();
                _redoStack.Push(activeCommand);
                activeCommand.Undo();
            }
        }

        /// <summary>
        /// 重做上一个撤销的命令
        /// </summary>
        public void RedoCommand()
        {
            if (_redoStack.Count > 0)
            {
                ICommand activeCommand = _redoStack.Pop();
                _undoStack.Push(activeCommand);
                activeCommand.Execute();
            }
        }
    }
}