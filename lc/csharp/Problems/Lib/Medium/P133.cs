namespace Lib.Medium;

public class P133
{
    public Node CloneGraph(Node node)
    {
        if (node == null) return null;

        HashSet<Node> nodeMap = new HashSet<Node>();
        
        Node CloneNode(Node n)
        {
            if (nodeMap.Any(x => x.val == n.val))
                return nodeMap.First(x => x.val == n.val);

            var newNode = new Node(n.val, new List<Node>());
            nodeMap.Add(newNode);

            foreach(var c in n.neighbors)
            {
                var newC = CloneNode(c);
                newNode.neighbors.Add(newC);
            }

            return newNode;
        }

        return CloneNode(node);
    }

    
}


//Definition for a Node.
public class Node
{
    public int val;
    public IList<Node> neighbors;

    public Node()
    {
        val = 0;
        neighbors = new List<Node>();
    }

    public Node(int _val)
    {
        val = _val;
        neighbors = new List<Node>();
    }

    public Node(int _val, List<Node> _neighbors)
    {
        val = _val;
        neighbors = _neighbors;
    }
}

