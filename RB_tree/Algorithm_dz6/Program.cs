namespace Algorithm_dz6;
class Program
{
    static void Main(string[] args)
    {
        RBTree tree = new RBTree();
        int[] nodes = { 27, 34, 17, 20, 10, 5, 15, 11, 14, 12, 16, 40, 33, 37 };
        foreach (int n in nodes) tree.InsertNode(n);
        Console.WriteLine(tree.ToString()); //Дерево после вставки элементов
        int[] d_noads = { 33, 15, 14 };
        foreach (int d in d_noads) tree.Delete(d);
        Console.WriteLine("\n***************************************************\n");
        Console.WriteLine(tree.ToString()); //Дерево полсе удаления элементов
        Console.ReadLine();
    }
}

