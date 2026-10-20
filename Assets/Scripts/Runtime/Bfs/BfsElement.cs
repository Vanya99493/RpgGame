namespace Runtime.Bfs
{
	public class BfsElement<T> where T : class, IBfsElement
	{
		public T Reference;
		public T ComeFromReference;
		public int Depth;
		
		public BfsElement(
			T reference,
			T comeFromReference,
			int depth)
		{
			Reference = reference;
			ComeFromReference = comeFromReference;
			Depth = depth;
		}
	}
}