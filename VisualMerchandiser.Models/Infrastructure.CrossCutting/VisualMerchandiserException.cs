using System;

namespace VisualMerchandiser.Models.Infrastructure.CrossCutting
{
    class VisualMerchandiserException : Exception
    {
        public VisualMerchandiserException()
        {
        }

        public VisualMerchandiserException(string message)
            : base(message)
        {
        }

        public VisualMerchandiserException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}