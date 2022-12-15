using Moq;
using SpaceBattleGame.Contracts.Commands;
using SpaceBattleGame.Contracts.Common;
using SpaceBattleGame.Server.Commands.Move;
using SpaceBattleGame.Server.Commands.Rotate;
using SpaceBattleGame.Server.Commands.Threads;
using System.Collections.Concurrent;

namespace SpaceBattleGame.Server.Tests.QueueProcessing
{
    public class QueueProcessingTests
    {
        /// <summary>
        /// тест, который проверяет, что после команды старт поток запущен
        /// </summary>
        [Fact]
        public void EventLoop_Start_StartThread()
        {            
            //Arrange
            Mock<IMovable> mockMovable = new Mock<IMovable>();
            mockMovable.SetupGet(m => m.Position).Returns(new Vector(12, 5));
            mockMovable.SetupGet(m => m.Velocity).Returns(new Vector(-7, 3));
            IoC.IoC.Resolve<ICommand>("IoC.Register",
                                      "MoveCommand",
                                      (object[] args) => new MoveCommand((IMovable)args[0])
                                      ).Execute();
            
            var queue = new ConcurrentQueue<ICommand>();
            var eventLoopCommand = new EventLoopCommand(queue);

            IoC.IoC.Resolve<ICommand>("IoC.Register",
                                      "EventLoop",
                                      (object[] args) => eventLoopCommand
                                      ).Execute();

            var moveCommand = IoC.IoC.Resolve<ICommand>("MoveCommand", mockMovable.Object);
            var eventLoop = (EventLoopCommand)IoC.IoC.Resolve<ICommand>("EventLoop");

            queue.Enqueue(moveCommand);            
            
            //Act
            eventLoop.Execute();
            //Даем немного времени на запуск потока
            Thread.Sleep(100);

            //Assert
            //проверяем, что если команда движения выполнилась
            mockMovable.VerifySet(m => m.Position = new Vector(5, 8));
            //проверяем, что поток выполнения запущен
            Assert.True(eventLoopCommand.IsStartedThread);
        }

        /// <summary>
        /// тест, который проверяет, что после команды hard stop, поток завершается
        /// </summary>
        [Fact]
        public void EventLoop_HardStop_StopThread()
        {
            //Arrange
            Mock<IMovable> mockMovable = new Mock<IMovable>();
            mockMovable.SetupGet(m => m.Position).Returns(new Vector(12, 5));
            mockMovable.SetupGet(m => m.Velocity).Returns(new Vector(-7, 3));
            IoC.IoC.Resolve<ICommand>("IoC.Register",
                                      "MoveCommand",
                                      (object[] args) => new MoveCommand((IMovable)args[0])
                                      ).Execute();

            Mock<IRotable> mockRotable = new Mock<IRotable>();
            mockRotable.SetupGet(r => r.Direction).Returns(1);
            mockRotable.SetupGet(r => r.AngularVelocity).Returns(2);
            mockRotable.SetupGet(r => r.DirectionsNumber).Returns(8);
            IoC.IoC.Resolve<ICommand>("IoC.Register",
                                      "RotateCommand",
                                      (object[] args) => new RotateCommand(mockRotable.Object)
                                      ).Execute();

            var queue = new ConcurrentQueue<ICommand>();
            var eventLoopCommand = new EventLoopCommand(queue);            

            IoC.IoC.Resolve<ICommand>("IoC.Register",
                                      "EventLoop",
                                      (object[] args) => eventLoopCommand
                                      ).Execute();

            IoC.IoC.Resolve<ICommand>("IoC.Register",
                                      "HardStopCommand",
                                      (object[] args) => new HardStopCommand((Action)args[0])
                                      ).Execute();            

            var moveCommand = IoC.IoC.Resolve<ICommand>("MoveCommand", mockMovable.Object);
            var rotateCommand = IoC.IoC.Resolve<ICommand>("RotateCommand", mockRotable.Object);
            var eventLoop = (EventLoopCommand)IoC.IoC.Resolve<ICommand>("EventLoop");
            var hardStopCommand = IoC.IoC.Resolve<ICommand>("HardStopCommand", eventLoop.Stop);
            
            queue.Enqueue(moveCommand);            
            queue.Enqueue(hardStopCommand);            
            queue.Enqueue(rotateCommand);

            //Act
            eventLoop.Execute();
            //Даем немного времени на запуск потока
            Thread.Sleep(100);

            //проверяем, что выполнилась команда движения
            mockMovable.VerifySet(m => m.Position = new Vector(5, 8));
            //проверяем, что команда поворота не выполнялась
            mockRotable.VerifyNoOtherCalls();
            //проверяем, что поток выполнения завершен, значит hardStopCommand выполнился
            Assert.False(eventLoopCommand.IsStartedThread);
        }

        /// <summary>
        /// тест, который проверяет, что после команды soft stop, поток завершается, после того как все команды завершат свою работу
        /// </summary>
        [Fact]
        public void EventLoop_SoftStop_StopThread()
        {
            //Arrange
            Mock<IMovable> mockMovable = new Mock<IMovable>();
            mockMovable.SetupGet(m => m.Position).Returns(new Vector(12, 5));
            mockMovable.SetupGet(m => m.Velocity).Returns(new Vector(-7, 3));
            IoC.IoC.Resolve<ICommand>("IoC.Register",
                                      "MoveCommand",
                                      (object[] args) => new MoveCommand((IMovable)args[0])
                                      ).Execute();

            Mock<IRotable> mockRotable = new Mock<IRotable>();
            mockRotable.SetupGet(r => r.Direction).Returns(1);
            mockRotable.SetupGet(r => r.AngularVelocity).Returns(2);
            mockRotable.SetupGet(r => r.DirectionsNumber).Returns(8);
            IoC.IoC.Resolve<ICommand>("IoC.Register",
                                      "RotateCommand",
                                      (object[] args) => new RotateCommand(mockRotable.Object)
                                      ).Execute();

            var queue = new ConcurrentQueue<ICommand>();
            var eventLoopCommand = new EventLoopCommand(queue);
            
            IoC.IoC.Resolve<ICommand>("IoC.Register",
                                      "EventLoop",
                                      (object[] args) => eventLoopCommand
                                      ).Execute();
            
            IoC.IoC.Resolve<ICommand>("IoC.Register",
                                      "SoftStopCommand",
                                      (object[] args) => new SoftStopCommand((EventLoopCommand)args[0])
                                      ).Execute();            

            var moveCommand = IoC.IoC.Resolve<ICommand>("MoveCommand", mockMovable.Object);
            var rotateCommand = IoC.IoC.Resolve<ICommand>("RotateCommand", mockRotable.Object);
            var eventLoop = (EventLoopCommand)IoC.IoC.Resolve<ICommand>("EventLoop");
            var softStopCommand = IoC.IoC.Resolve<ICommand>("SoftStopCommand", eventLoopCommand);

            queue.Enqueue(moveCommand);
            queue.Enqueue(softStopCommand);
            queue.Enqueue(rotateCommand);

            //Act
            eventLoop.Execute();
            //Даем немного времени на запуск потока            
            Thread.Sleep(100);

            //Assert
            //если команда rotateCommand выполнилась значит предыдущая команда softStopCommand правильно выполнилась
            mockRotable.VerifySet(r => r.Direction = 3);
            //проверяем остановлен ли цикл обработки команд
            Assert.False(eventLoopCommand.IsStartedThread);
        }
    }
}
