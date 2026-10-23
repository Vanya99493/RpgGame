namespace Runtime.Bfs
{
	public class BfsElement<T> where T : class, IBfsElement
	{
		public T Reference;
		public BfsElement<T> ComeFromElement;
		public int Depth;
		
		public BfsElement(
			T reference,
			BfsElement<T> comeFromElement,
			int depth)
		{
			Reference = reference;
			ComeFromElement = comeFromElement;
			Depth = depth;
		}
	}
}