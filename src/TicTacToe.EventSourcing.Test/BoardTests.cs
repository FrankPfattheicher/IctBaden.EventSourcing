using System.Linq;
using IctBaden.EventSourcing;
using IctBaden.EventSourcing.EventStore;
using TicTacToe.EventSourcing.Wpf.Game.Commands;
using TicTacToe.EventSourcing.Wpf.Game.Contexts;
using Xunit;

namespace TicTacToe.EventSourcing.Test
{
    public class BoardTests
    {
        private readonly EventContext _context;
        
        public BoardTests()
        {
            var store = new InMemoryEventStore();
            var publisher = new AppDomainEventPublisher(new []{ typeof(PlayerSetCommand).Assembly });
            _context = new EventContext("test", store, publisher);
        }
        
        [Fact]
        public void InitialBoardShouldBeEmpty()
        {
            var board = _context.GetContextInstance<BoardContext>();
            Assert.True(board.Board.All(b => b.All(f => f == " ")));
        }

        [Fact]
        public void AfterFirstPlayerBoardShouldNotBeEmpty()
        {
            _context.ExecuteCommand(new PlayerSetCommand("X", 1, 1));
            
            var board = _context.GetContextInstance<BoardContext>();
            Assert.False(board.Board.All(b => b.All(f => f == " ")));
        }
        
        
        
        
    }
}