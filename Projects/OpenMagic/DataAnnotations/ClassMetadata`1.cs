namespace OpenMagic.DataAnnotations
{
    public class ClassMetadata<T> : ClassMetadata
    {
        public ClassMetadata() : base(typeof(T))
        {
        }
    }
}