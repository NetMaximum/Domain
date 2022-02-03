// using System;
// using System.Collections.Generic;
// using System.Reflection;
// using System.Threading.Tasks;
// using NetMaximum.Domain.EventSourced;
//
// namespace NetMaximum.Domain.UnitTests.Example;
//
// public class FakeAggregateStore : EventSourcedRepository
// {
//     private readonly Dictionary<string, IEnumerable<object>> _store = new();
//
//     public FakeAggregateStore(IEventSourcedAggregateStore store) : base(store)
//     {
//     }
//
//     protected override Task<bool> ExistsAsync<T>(AggregateId<T> aggregateId) 
//     {
//         return Task.FromResult(_store.ContainsKey(GetStreamName(aggregateId)));
//     }
//
//     public Task SaveAsync<T>(T aggregate) where T : EventSourcedAggregateRoot
//     {
//         _store.Add(GetStreamName(aggregate), aggregate.GetEvents());
//         return Task.CompletedTask;
//     }
//
//     public Task<Optional<T>> LoadAsync<T>(AggregateId<T> aggregateId) where T : EventSourcedAggregateRoot
//     {
//         // var c= typeof(T).GetConstructor(BindingFlags.NonPublic | BindingFlags.CreateInstance | BindingFlags.Instance, 
//         //     null, 
//         //     new[] { typeof(Guid) }, 
//         //     null
//         // );
//         //
//         // var aggregate = (T)c.Invoke(new object[] {aggregateId.Value});
//         //
//         // if (aggregate == null)
//         // {
//         //     return Task.FromResult(Optional<T>.None);
//         // }
//         //
//         // aggregate.Load(new List<object>());
//         // return Task.FromResult(Optional<T>.Some(aggregate));
//
//         return Task.FromResult(Optional<T>.None);
//     }
//
//     public Task DeleteAsync<T>(AggregateId<T> aggregateId) where T : EventSourcedAggregateRoot
//     {
//         throw new System.NotImplementedException();
//     }
//
//     static string GetStreamName<T>(AggregateId<T> aggregateId)
//         where T : AggregateRoot
//         => $"{typeof(T).Name}-{aggregateId}";
//
//     static string GetStreamName<T>(T aggregate)
//         where T : AggregateRoot
//         => $"{typeof(T).Name}-{aggregate.Id.ToString()}";
//     
// }