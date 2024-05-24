namespace Lib.Medium;

public class P102
{
    public IList<IList<int>> LevelOrder(TreeNode root)
    {
        if(root == null)
            return new List<IList<int>>();

        var queue = new Queue<TreeNode>();
        queue.Enqueue(root);
        var result = new List<IList<int>>();

        while(queue.Count > 0)
        {
            var levelQueue = new List<TreeNode>();
            var level = new List<int>();

            while(queue.Count > 0)
            {
                var node = queue.Dequeue();
                level.Add(node.val);

                if (node.left != null) levelQueue.Add(node.left);
                if (node.right != null) levelQueue.Add(node.right);
            }

            if(level.Any())
            {
                result.Add(level);
            }
            foreach(var node in levelQueue)
            {
                queue.Enqueue(node);
            }
        }

        return result;
    }
}

public class TreeNode
{
    public int val;
    public TreeNode left;
    public TreeNode right;
    public TreeNode(int val = 0, TreeNode left = null, TreeNode right = null)
    {
        this.val = val;
        this.left = left;
        this.right = right;
    }
}

