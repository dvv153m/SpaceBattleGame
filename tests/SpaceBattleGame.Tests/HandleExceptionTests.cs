using Moq;
using SpaceBattleGame.Server.Commands.Move;
using SpaceBattleGame.Server.Exceptions;
using SpaceBattleGame.Server.Exceptions.CommandsExceptionHandler;
using ICommand = SpaceBattleGame.Server.Commands.ICommand;

namespace SpaceBattleGame.Server.Tests
{
    public class HandleExceptionTests
    {
        /// <summary>
        /// Тест на пункт 4: Реализовать Команду, которая записывает информацию о выброшенном исключении в лог.
        /// </summary>
        [Fact]
        public void LogCommand_Execute_OutputConsole()
        {
            //Arrange
            Mock<IMovable> mockMovable = new Mock<IMovable>();
            var logCommand = new LogCommand(new MoveCommand(mockMovable.Object), new ArgumentNullException());
            var expectedText = $"throw exception in MoveCommand. Exception details: System.ArgumentNullException: Value cannot be null.{Environment.NewLine}";

            //Act
            using (var consoleText = new StringWriter())
            {
                Console.SetOut(consoleText);
                logCommand.Execute();
                
                //Assert
                Assert.Equal(consoleText.ToString(), expectedText);                
            }            
        }

        /// <summary>
        /// Тест на пункт 5: Реализовать обработчик исключения, который ставит Команду, пишущую в лог в очередь Команд.
        /// </summary>
        [Fact]
        public void ExceptionHandler_MoveCommandPassInExceptionHandler_AddMoveCommandInQueueCommands()
        {
            //Arrange
            Mock<IMovable> mockMovable = new Mock<IMovable>();
            Queue<ICommand> _queueCommands = new Queue<ICommand>();
            var moveCommand = new MoveCommand(mockMovable.Object);
            
            var exceptionHandler = new ExceptionHandler();            
            exceptionHandler.Setup(typeof(MoveCommand), typeof(ArgumentNullException), (cmd, ex) => _queueCommands.Enqueue(new LogCommand(cmd, ex)));
            var expectedCountCommand = 1;

            //Act
            exceptionHandler.Handle(moveCommand, new ArgumentNullException());            

            //Assert
            Assert.Equal(_queueCommands.Count, expectedCountCommand);
        }

        /// <summary>
        /// Тест на пункт 6: Реализовать Команду, которая повторяет Команду, выбросившую исключение.
        /// </summary>
        [Fact]
        public void ExceptionHandler_OneCommand_RepeatCommand()
        {
            //Arrange            
            Mock<ICommand> mockCommand = new Mock<ICommand>();
            mockCommand.Setup(x => x.Execute()).Throws(new ArgumentNullException());
            var oneCommand = new OneCommand(mockCommand.Object);

            //Act                                    
            var exception = Assert.Throws<ArgumentNullException>(
            () =>
            {
                oneCommand.Execute();
            });

            //Assert
            Assert.NotNull(exception);            
        }

        /// <summary>
        /// Тест на пункт 7: Реализовать обработчик исключения, который ставит в очередь Команду - повторитель команды, выбросившей исключение
        /// </summary>
        [Fact]
        public void ExceptionHandler_Execute_OutputConsole2()
        {
            //Arrange
            Mock<IMovable> mockMovable = new Mock<IMovable>();
            Queue<ICommand> queueCommands = new Queue<ICommand>();
            var moveCommand = new MoveCommand(mockMovable.Object);
      
            var exceptionHandler = new ExceptionHandler();
            exceptionHandler.Setup(typeof(MoveCommand), typeof(ArgumentNullException), (cmd, ex) => queueCommands.Enqueue(new OneCommand(cmd)));
            var expectedCountCommand = 1;

            //Act
            exceptionHandler.Handle(moveCommand, new ArgumentNullException());
            
            //Assert
            Assert.Equal(queueCommands.Count, expectedCountCommand);
        }

        /// <summary>
        /// Тест на пункт 8: С помощью Команд из пункта 4 и пункта 6 реализовать следующую обработку исключений:
        ///                  при первом выбросе исключения повторить команду, при повторном выбросе исключения записать информацию в лог.
        /// </summary>
        [Fact]
        public void ExceptionHandler_MoveCommandThrowException_RepeatCommandAndOutputConsoleException()
        {            
            //Arrange
            Mock<ICommand> mockCommand = new Mock<ICommand>();            
            mockCommand.Setup(x => x.ToString()).Returns("MoveCommand");                        
            mockCommand.Setup(x => x.Execute()).Throws(new ArgumentNullException());            

            Queue<ICommand> queueCommands = new Queue<ICommand>();
            queueCommands.Enqueue(mockCommand.Object);

            var exceptionHandler = new ExceptionHandler();
            exceptionHandler.Setup(typeof(MoveCommand), typeof(ArgumentNullException), (cmd, ex) => queueCommands.Enqueue(new OneCommand(cmd)));
            exceptionHandler.Setup(typeof(OneCommand), typeof(ArgumentNullException), (cmd, ex) => queueCommands.Enqueue(new LogCommand(cmd, ex)));

            //текст, который должен вывестись в консоль
            var expectedText = "throw exception in OneCommand. Exception details: System.ArgumentNullException: Value cannot be null.";
            //должно выполнится 3 команды: MoveCommand, OneCommand, LogCommand
            int expectedCommandCount = 3;

            //Act
            using (var consoleText = new StringWriter())
            {
                Console.SetOut(consoleText);

                int commandCount = 0;
                while (queueCommands.TryDequeue(out ICommand cmd))
                {
                    try
                    {
                        commandCount++;
                        cmd.Execute();
                    }
                    catch (Exception ex)
                    {
                        exceptionHandler.Handle(cmd, ex);
                    }
                }

                //Assert            
                Assert.True(consoleText.ToString().StartsWith(expectedText));
                Assert.True(expectedCommandCount == commandCount);
            }            
        }

        /// <summary>
        /// Тест на пункт 9: Реализовать стратегию обработки исключения - повторить два раза, потом записать в лог.
        ///                  Указание: создать новую команду, точно такую же как в пункте 6. Тип этой команды будет показывать, что Команду не удалось выполнить два раза.
        /// </summary>
        [Fact]
        public void ExceptionHandler_MoveCommandThrowException_RepeatTwoCommandAndOutputConsoleException()
        {            
            //Arrange
            Mock<ICommand> mockCommand = new Mock<ICommand>();
            mockCommand.Setup(x => x.ToString()).Returns("MoveCommand");
            mockCommand.Setup(x => x.Execute()).Throws(new ArgumentNullException());

            Queue<ICommand> queueCommands = new Queue<ICommand>();
            queueCommands.Enqueue(mockCommand.Object);

            var exceptionHandler = new ExceptionHandler();
            exceptionHandler.Setup(typeof(MoveCommand), typeof(ArgumentNullException), (cmd, ex) => queueCommands.Enqueue(new TwoCommand(cmd)));
            exceptionHandler.Setup(typeof(TwoCommand), typeof(ArgumentNullException), (cmd, ex) => queueCommands.Enqueue(new OneCommand(cmd)));
            exceptionHandler.Setup(typeof(OneCommand), typeof(ArgumentNullException), (cmd, ex) => queueCommands.Enqueue(new LogCommand(cmd, ex)));

            //текст, который должен вывестись в консоль
            var expectedText = "throw exception in OneCommand. Exception details: System.ArgumentNullException: Value cannot be null.";
            //должно выполнится 4 команды: MoveCommand, TwoCommand, OneCommand, LogCommand
            int expectedCommandCount = 4;

            //Act
            using (var consoleText = new StringWriter())
            {
                Console.SetOut(consoleText);

                int commandCount = 0;
                while (queueCommands.TryDequeue(out ICommand cmd))
                {
                    try
                    {
                        commandCount++;
                        cmd.Execute();
                    }
                    catch (Exception ex)
                    {
                        exceptionHandler.Handle(cmd, ex);
                    }
                }

                //Assert            
                Assert.True(consoleText.ToString().StartsWith(expectedText));
                Assert.True(expectedCommandCount == commandCount);
            }
        }        
    }
}
