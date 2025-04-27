using System;
using System.Collections.Generic;
using System.IO;
using Xunit;
using CA5;
using Assert = Xunit.Assert;

namespace CA5Tests
{
    public class AVLTreeTests
    {
        private List<int> GetInOrderTraversal(AVLTree tree)
        {
            var result = new List<int>();
            var originalOut = Console.Out;
            using (var writer = new StringWriter())
            {
                Console.SetOut(writer);
                tree.inOrder();
                Console.SetOut(originalOut);
                var output = writer.ToString().Trim();
                foreach (var item in output.Split(' ', StringSplitOptions.RemoveEmptyEntries))
                {
                    result.Add(int.Parse(item));
                }
            }
            return result;
        }

        [Fact]
        public void Insert_SingleNode_ShouldAppearInOrder()
        {
            AVLTree tree = new AVLTree();
            tree.insert(10);

            var result = GetInOrderTraversal(tree);

            Assert.Single(result);
            Assert.Equal(10, result[0]);
        }

        [Fact]
        public void Insert_MultipleNodes_ShouldAppearSortedInOrder()
        {
            AVLTree tree = new AVLTree();
            tree.insert(20);
            tree.insert(10);
            tree.insert(30);

            var result = GetInOrderTraversal(tree);

            Assert.Equal(new List<int> { 10, 20, 30 }, result);
        }

        [Fact]
        public void Delete_Node_ShouldNotAppearInOrder()
        {
            AVLTree tree = new AVLTree();
            tree.insert(20);
            tree.insert(10);
            tree.insert(30);

            tree.delete(20);
            var result = GetInOrderTraversal(tree);

            Assert.Equal(new List<int> { 10, 30 }, result);
        }

        [Fact]
        public void Insert_Causes_LeftLeftRotation()
        {
            AVLTree tree = new AVLTree();
            tree.insert(30);
            tree.insert(20);
            tree.insert(10); // Esto debería causar una rotación simple a la derecha.

            var result = GetInOrderTraversal(tree);

            Assert.Equal(new List<int> { 10, 20, 30 }, result);
        }

        [Fact]
        public void Insert_Causes_RightRightRotation()
        {
            AVLTree tree = new AVLTree();
            tree.insert(10);
            tree.insert(20);
            tree.insert(30); // Esto debería causar una rotación simple a la izquierda.

            var result = GetInOrderTraversal(tree);

            Assert.Equal(new List<int> { 10, 20, 30 }, result);
        }

        [Fact]
        public void Insert_Causes_LeftRightRotation()
        {
            AVLTree tree = new AVLTree();
            tree.insert(30);
            tree.insert(10);
            tree.insert(20); // Esto debería causar rotación izquierda-derecha.

            var result = GetInOrderTraversal(tree);

            Assert.Equal(new List<int> { 10, 20, 30 }, result);
        }

        [Fact]
        public void Insert_Causes_RightLeftRotation()
        {
            AVLTree tree = new AVLTree();
            tree.insert(10);
            tree.insert(30);
            tree.insert(20); // Esto debería causar rotación derecha-izquierda.

            var result = GetInOrderTraversal(tree);

            Assert.Equal(new List<int> { 10, 20, 30 }, result);
        }

        private void CheckBalance(AVLTree tree)
        {
            Assert.True(IsBalanced(tree), "El árbol no está balanceado.");
        }

        private bool IsBalanced(AVLTree tree)
        {
            // Usamos reflexión para acceder a la raíz (private field)
            var rootField = typeof(AVLTree).GetField("root", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
            var root = rootField.GetValue(tree) as AVLNode;

            return CheckBalancedRecursive(root) != -1;
        }

        private int CheckBalancedRecursive(AVLNode node)
        {
            if (node == null) return 0;

            int leftHeight = CheckBalancedRecursive(node.left);
            if (leftHeight == -1) return -1;

            int rightHeight = CheckBalancedRecursive(node.right);
            if (rightHeight == -1) return -1;

            if (Math.Abs(leftHeight - rightHeight) > 1)
                return -1; // No balanceado

            return Math.Max(leftHeight, rightHeight) + 1;
        }

        [Fact]
        public void Insert_MultipleNodes_ShouldRemainBalanced()
        {
            AVLTree tree = new AVLTree();
            int[] values = { 10, 20, 30, 40, 50, 25 };

            foreach (var val in values)
            {
                tree.insert(val);
                CheckBalance(tree);
            }
        }

        [Fact]
        public void Delete_Node_ShouldRemainBalanced()
        {
            AVLTree tree = new AVLTree();
            int[] values = { 10, 20, 30, 40, 50, 25 };
            foreach (var val in values)
            {
                tree.insert(val);
            }

            tree.delete(30); // Elimina un nodo intermedio
            CheckBalance(tree);

            tree.delete(40); // Elimina otro nodo
            CheckBalance(tree);
        }
    }
}
