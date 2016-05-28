using System;
using WebEasyEventSourcing.EventSourcing.Domain;

namespace WebEasyEventSourcing.EventSourcing.Persistence
{
    public interface IRepository
    {
        T GetById<T>(Guid id) where T : EventStream, new();

        void Save(params EventStream[] streamItems);
    }
}