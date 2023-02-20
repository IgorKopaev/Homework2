using System;
namespace Algorithm_dz6
{
	public class Node
	{
		public Node? Left { get; set; }
		public Node? Right { get; set; }
		public Node? Parent { get; set; }
		public bool Red { get; set; } = false;
		public int Key { get; set; }

		public Node() { }

		public Node(int Key)
		{
			this.Key = Key;
		}
	}

	public class RBTree
	{
		public Node? Nil { get; set; }
		public Node? Root { get; set; }

		public RBTree()
		{
			Nil = new Node();
			Root = Nil;
		}

		public void InsertNode(int key)
		{
			Node new_node = new Node(key) { Left = Nil, Right = Nil, Red = true };

			Node parent = null;
			Node current = Root;
			while (current != Nil)
			{
				parent = current;
				if (new_node.Key < current.Key)
					current = current.Left;
				else if (new_node.Key > current.Key)
					current = current.Right;
				else return;
			}

			new_node.Parent = parent;
			if (parent == null)
				Root = new_node;
			else if (new_node.Key < parent.Key)
				parent.Left = new_node;
			else
				parent.Right = new_node;

			FixInsert(new_node);

		}

		public void FixInsert(Node new_node)
		{
			while (new_node != Root && new_node.Parent.Red)
			{
				if (new_node.Parent == new_node.Parent.Parent.Right)
				{
					Node uncle = new_node.Parent.Parent.Left;
					if (uncle.Red == true)
					{
						new_node.Parent.Red = false;
						uncle.Red = false;
						uncle.Parent.Red = true;
						new_node = uncle.Parent;
					}
					else
					{
						if (new_node == new_node.Parent.Left)
						{
							new_node = new_node.Parent;
							RightRotate(new_node);
						}
						new_node.Parent.Red = false;
						new_node.Parent.Parent.Red = true;
						LeftRotate(new_node.Parent.Parent);
					}
				}
				else
				{
                    Node uncle = new_node.Parent.Parent.Right;
                    if (uncle.Red == true)
                    {
                        new_node.Parent.Red = false;
                        uncle.Red = false;
                        uncle.Parent.Red = true;
                        new_node = uncle.Parent;
                    }
                    else
                    {
                        if (new_node == new_node.Parent.Right)
                        {
                            new_node = new_node.Parent;
                            LeftRotate(new_node);
                        }
                        new_node.Parent.Red = false;
                        new_node.Parent.Parent.Red = true;
                        RightRotate(new_node.Parent.Parent);
                    }
                }
			}
			Root.Red = false;
		}

		public void Delete(int key)
		{
			Node current = Root;
			while (current != Nil && key != current.Key)
			{
				if (key < current.Key)
					current = current.Left;
				else if (key > current.Key)
					current = current.Right;

			}
			DeleteNode(key, current);

		}

		public void DeleteNode(int key, Node current)
		{
			if (current == Nil) return;
			if (current.Red == true && current.Left == Nil && current.Right == Nil)
			{
				//Case 1
				if (current == current.Parent.Left)
					current.Parent.Left = Nil;
				else
					current.Parent.Right = Nil;
			}
			else if(current.Red == false && current.Left == Nil && current.Right == Nil)
			{
				//Случай с балансировкой
				if (current == current.Parent.Left)
				{
                    FixDelete(current);
                    current.Parent.Left = Nil;
				}
				else
				{
                    FixDelete(current);
                    current.Parent.Right = Nil;
				}
            }
			else if (current.Red == false && (current.Left == Nil || current.Right == Nil))
			{
				//Case 2
				if (current.Left == Nil)
				{
                    (current.Right.Key, current.Key) = (current.Key, current.Right.Key);
                    DeleteNode(key, current.Right);
				}
				else
				{
                    (current.Left.Key, current.Key) = (current.Key, current.Left.Key);
                    DeleteNode(key, current.Left);
				}
			}
			else
			{
				Node temp = current.Left;
				while (temp.Right != Nil)
					temp = temp.Right;
				(current.Key, temp.Key) = (temp.Key, current.Key);
				DeleteNode(key, temp);

			}

		}

		public void FixDelete(Node X)
		{
			Node parent = X.Parent;
			if (X == X.Parent.Left)
			{
				Node brother = parent.Right;
				if (brother.Red == false) //1.
				{
					if (brother.Right.Red == true) //1.1.a
					{
						brother.Red = parent.Red;
						parent.Red = false;
						brother.Right.Red = false;
						LeftRotate(parent);
					}
					else if (brother.Left.Red == true && brother.Right.Red == false) //1.1b
					{
						brother.Red = true;
						brother.Left.Red = false;
						RightRotate(brother);
						FixDelete(X);
					}
					else if (brother.Left.Red == false && brother.Right.Red == false) //1.2
					{
						brother.Red = true;
						if (parent.Red == true) return;
						else FixDelete(parent);
					}
				}
				else //2.
				{
					parent.Red = true;
					brother.Red = false;
					LeftRotate(parent);
					FixDelete(X);
				}
			}
            if (X == X.Parent.Right)
            {
                Node brother = parent.Left;
                if (brother.Red == false) //1.
                {
                    if (brother.Left.Red == true) //1.1.a
                    {
                        brother.Red = parent.Red;
                        parent.Red = false;
                        brother.Left.Red = false;
                        RightRotate(parent);
                    }
                    else if (brother.Right.Red == true && brother.Left.Red == false) //1.1b
                    {
                        brother.Red = true;
                        brother.Right.Red = false;
                        LeftRotate(brother);
                        FixDelete(X);
                    }
                    else if (brother.Right.Red == false && brother.Left.Red == false) //1.2
                    {
                        brother.Red = true;
                        if (parent.Red == true) return;
                        else FixDelete(parent);
                    }
                }
                else //2.
                {
                    parent.Red = true;
                    brother.Red = false;
                    RightRotate(parent);
                    FixDelete(X);
                }
            }
        }

		public void LeftRotate(Node x)
		{
			Node y = x.Right;
			x.Right = y.Left;
			if (y.Left != Nil)
				y.Left.Parent = x;
			y.Parent = x.Parent;
			if (x.Parent == null)
				Root = y;
			else if (x == x.Parent.Left)
				x.Parent.Left = y;
			else
				x.Parent.Right = y;
			y.Left = x;
			x.Parent = y;
		}

        public void RightRotate(Node x)//7
        {
            Node y = x.Left;
            x.Left = y.Right;
            if (y.Right != Nil)
                y.Right.Parent = x;// Error!!!!
            y.Parent = x.Parent;
            if (x.Parent == null)
                Root = y;
            else if (x == x.Parent.Right)
                x.Parent.Right = y;
            else
                x.Parent.Left = y;
            y.Right = x;
            x.Parent = y;
        }

		public void PrintTree(Node node, List<string> lines, int level = 0)
		{
			if (node.Key != 0)
			{
				PrintTree(node.Left, lines, level + 1);
				string represent = "-".Repeat(4 * level) + ">" + node.Key.ToString() +
					" " + (node.Red ? "r" : "b");
				lines.Add(represent);
				PrintTree(node.Right, lines, level + 1);
			}
		}

        public override string ToString()
        {
			List<string> lines = new List<string>();
			PrintTree(Root, lines);
            return String.Join("\n", lines.ToArray());
        }
    }

    public static class StringExtensions
    {
        public static string Repeat(this string value, int count) => string.Concat(Enumerable.Repeat(value, count));
    }
}

