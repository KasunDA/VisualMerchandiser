using System;
using System.Data.Entity;

namespace VisualMerchandiser.Models.Domain
{
    public static class StateExtensions
    {
        public static EntityState ConvertToEntityState(this State state)
        {
            switch (state)
            {
                case State.Added:
                    return EntityState.Added;
                case State.Deleted:
                    return EntityState.Deleted;
                case State.Modified:
                    return EntityState.Modified;
                case State.Unchanged:
                    return EntityState.Unchanged;
                default:
                    throw new ArgumentOutOfRangeException(nameof(state), state, null);
            }
        }
    }
}