using System.Threading.Tasks;

namespace EventStream.Domain.Interfaces
{
    public interface IEventStreamClient
    {
        public void Connect();
    }
}