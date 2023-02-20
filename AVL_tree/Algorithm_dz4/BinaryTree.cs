using System;
namespace Algorithm_dz4
{
	class Node
	{
		public int Key { get; set; }
		public Node? Left { get; set; }
		public Node? Right { get; set; }
		public int Height { get; set; }

		public Node() { }

		public Node(int Key)
		{
			this.Key = Key;
		}
	}

	class BinaryTree
	{
		public Node? Root { get; set; }

		public BinaryTree() { }

		public BinaryTree(Node Root)
		{
			this.Root = Root;
		}

		public int height(Node node)
		{
			return (node == null ? -1 : node.Height);
		}

		public Node GetNode(Node node, int key, ref Node p)
		{
			/*
			 * Задаёт объекту класса Node (переменная p) указанное значение из
			 * бинарного древа поиска.
			 */
			if (node == null) return null;
			if (node.Key == key)
			{
				p = node;
				return node;
			}
			else
			{
				if (key < node.Key)
				{
					node = node.Left;
					GetNode(node, key, ref p);
				}
				else
				{
					node = node.Right;
					GetNode(node, key, ref p);
				}
			}
			return node;

		}

		public Node InsertNode(Node node, int key)
		{
			if (node == null)
				return new Node(key) { Height = 0 };
			else if (key < node.Key)
			{
				node.Left = InsertNode(node.Left, key);
				if (height(node.Left) - height(node.Right) == 2)
				{
					if (key < node.Left.Key) node = RotateRight(node);
					else node = BigRotateRight(node);
				}
			}
			else if (key > node.Key)
			{
				node.Right = InsertNode(node.Right, key);
				if (height(node.Right) - height(node.Left) == 2)
				{
					if (key > node.Right.Key) node = RotateLeft(node);
					else node = BigRotateLeft(node);
				}
			}
			node.Height = Math.Max(height(node.Left), height(node.Right)) + 1;
			return node;
		}

		public Node DeleteNode(Node node, int key)
		{
			if (node == null) return node;

            else if (key < node.Key) node.Left = DeleteNode(node.Left, key);
            else if (key > node.Key) node.Right = DeleteNode(node.Right, key);
			else if (node.Left == null)
			{
				Node temp = node.Right;
				node = null;
				return temp;

			}
			else if (node.Right == null)
			{
				Node temp = node.Left;
				node = null;
				return temp;
			}
			else if (node.Left != null && node.Right != null)
			{
				Node temp1 = MinValTree(node.Right);
				node.Key = temp1.Key;
				node.Right = DeleteNode(node.Right, temp1.Key);
			}

			node.Height = Math.Max(height(node.Left), height(node.Right)) + 1;
			if (height(node.Left) - height(node.Right) == -2)
			{
				if (height(node.Left.Left) - height(node.Left.Right) == 1)
					return RotateLeft(node);
				else
					return BigRotateLeft(node);
			}
			else if (height(node.Right) - height(node.Left) == -2)
			{
                if (height(node.Right.Right) - height(node.Right.Left) == 1)
                    return RotateRight(node);
                else
                    return BigRotateRight(node);
            }

            return node;

        }

		public Node MinValTree(Node node)
		{
			Node current = node;
			while (current.Left != null) current = current.Left;
			return current;
		}

		public void InorderTree(Node? node)
		{
			if (node == null) return;
			InorderTree(node.Left);
			Console.Write($"Node: { node.Key}; Height: {node.Height} | ");
			InorderTree(node.Right);
		}

		public Node RotateLeft(Node a)
		{
			Node b = a.Right;
			a.Right = b.Left;
			b.Left = a;
			a.Height = Math.Max(height(a.Left), height(a.Right)) + 1;
			b.Height = Math.Max(height(a.Right), a.Height) + 1;
			return b;
		}

        public Node RotateRight(Node a)
        {
            Node b = a.Left;
            a.Left = b.Right;
            b.Right = a;
            a.Height = Math.Max(height(a.Left), height(a.Right)) + 1;
            b.Height = Math.Max(height(b.Left), a.Height) + 1;
            return b;
        }

        public Node BigRotateLeft(Node a)
		{
			a.Right = RotateRight(a.Right);
			return RotateLeft(a);
		}

        public Node BigRotateRight(Node a)
        {
            a.Left = RotateLeft(a.Left);
            return RotateRight(a);
        }
    }
}

