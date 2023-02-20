namespace Algorithm_dz4;
class Program
{
    static void Main(string[] args)
    {
        BinaryTree tree = new BinaryTree();
        int[] noads = {27, 34, 17, 20, 10, 5, 15, 11, 14, 12, 16, 40, 33, 37 };
        foreach (int n in noads) tree.Root = tree.InsertNode(tree.Root, n);
        Console.WriteLine("АВЛ дерево после добавления элементов");
        tree.InorderTree(tree.Root);
        Console.WriteLine("\n-----------------------------------------------------");
        int[] d_noads = { 33, 15, 14 };
        foreach (int n in d_noads) tree.Root = tree.DeleteNode(tree.Root, n);
        Console.WriteLine("АВЛ дерево после удаления элементов");
        tree.InorderTree(tree.Root);
        //Console.WriteLine(tree.Root.Right.Left.Key);
        Console.WriteLine();
        Node f = new Node();
        tree.GetNode(tree.Root, 27, ref f);
        Console.WriteLine(f.Left.Key);
        Console.ReadLine();
    }
}

