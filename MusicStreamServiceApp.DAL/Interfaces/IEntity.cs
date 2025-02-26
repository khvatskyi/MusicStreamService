using System;
using System.Collections.Generic;
using System.Text;

namespace MusicStreamServiceApp.DAL.Interfaces
{
    public interface IEntity<T>
    {
        T Id { get; set; }
    }
}
