using System.Collections;
using System.Collections.Generic;
using System.Windows.Input;

namespace Tools.GameProgrammingPatterns.Command
{
    /// <summary>
    /// 命令
    /// </summary>
    public interface ICommand
    {
        /// <summary>
        /// 执行命令
        /// </summary>
        void Execute();

        /// <summary>
        /// 撤销命令
        /// </summary>
        void Undo();
    }
}