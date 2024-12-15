using System.Collections.Generic;

namespace AtlasToolbox.Services
{
    public interface IBcdService
    {
        void DeleteElement(string objectId, uint elementType);
        object? GetElementValue(string objectId, uint elementType);
        void SetBooleanElement(string objectId, uint elementType, bool value);
        void SetIntegerElement(string objectId, uint elementType, ulong value);
        void SetIntegerListElement(string objectId, uint elementType, IEnumerable<ulong> value);
        void SetObjectElement(string objectId, uint elementType, string value);
        void SetObjectListElement(string objectId, uint elementType, IEnumerable<string> value);
        void SetStringElement(string objectId, uint elementType, string value);
    }
}
