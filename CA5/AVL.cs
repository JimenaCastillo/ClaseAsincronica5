using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CA5
{
    public class AVLNode
    {
        public int key, height;
        public AVLNode left, right;

        public AVLNode(int key)
        {
            this.key = key;
            this.height = 1;
            this.left = this.right = null;
        }
    }

    public class AVLTree
    {
        private AVLNode root;
        private int height(AVLNode node)
        {
            if (node == null)
                return 0;
            return node.height;
        }

        private int getBalance(AVLNode node)
        {
            if (node == null)
                return 0;
            return height(node.left) - height(node.right);
        }

        //Rotación derecha: y es el hijo izquierdo de z y x es el hijo izquierdo de y.
        private AVLNode rightRotate(AVLNode y)
        {
            AVLNode x = y.left;
            AVLNode z = x.right;

            x.right = y;
            y.left = z;

            y.height = Math.Max(height(y.left), height(y.right)) + 1;
            x.height = Math.Max(height(x.left), height(x.right)) + 1;

            return x;
        }

        //Rotación Izquierda: y es el hijo derecho de z y x es el hijo derecho de y.
        private AVLNode leftRotate(AVLNode x)
        {
            AVLNode y = x.right;
            AVLNode z = y.left;

            y.left = x;
            x.right = z;

            x.height = Math.Max(height(x.left), height(x.right)) + 1;
            y.height = Math.Max(height(y.left), height(y.right)) + 1;

            return y;
        }

        public void insert(int key)
        {
            root = insertRecursive(root, key);
        }

        //Inseta un nuevo nodo
        private AVLNode insertRecursive(AVLNode node, int key)
        {
            if(node == null)
                return new AVLNode(key);
            if(key < node.key)
                node.left = insertRecursive(node.left, key);
            else if(key > node.key)
                node.right = insertRecursive(node.right, key);
            else
                return node;

            node.height = 1 + Math.Max(height(node.left), height(node.right));

            int balance = getBalance(node);

            //rotaciones en caso de desbalance 
            //izq izq 
            if (balance > 1 && key < node.left.key)
                return rightRotate(node);

            //der der
            if(balance < -1 && key > node.right.key)
                return leftRotate(node);

            //izq der
            if(balance > 1 && key > node.left.key)
            {
                node.left = leftRotate(node.left);
                return rightRotate(node);
            }

            //der izq
            if(balance < -1 && key < node.right.key)
            {
                node.right = rightRotate(node.right);
                return leftRotate(node);
            }

            return node;
        }

        //Elimina un nodo
        public void delete(int key)
        {
            root = deleteRecursive(root, key);
        }

        private AVLNode deleteRecursive(AVLNode node, int key)
        {
            if(node == null)
                return node;
            if(key < node.key)
                node.left = deleteRecursive(node.left, key);
            else if(key > node.key)
                node.right = deleteRecursive(node.right, key);
            else
            {
                if(node.left == null)
                    return node.right;
                else if(node.right == null)
                    return node.left;

                node.key = MinValue(node.right);
                node.right = deleteRecursive(node.right, node.key);
            }

            node.height = 1 + Math.Max(height(node.left), height(node.right));

            int balance = getBalance(node);

            //rotaciones en caso de desbalance 
            // izq izq
            if(balance > 1 && getBalance(node.left) >= 0)
                return rightRotate(node);

            // izq der
            if(balance > 1 && getBalance(node.left) < 0)
            {
                node.left = leftRotate(node.left);
                return rightRotate(node);
            }

            // der der
            if(balance < -1 && getBalance(node.right) <= 0)
                return leftRotate(node);

            // der izq
            if(balance < -1 && getBalance(node.right) > 0)
            {
                node.right = rightRotate(node.right);
                return leftRotate(node);
            }

            return node;
        }

        //Funcion auxiliar para delete 
        private int MinValue(AVLNode node)
        {
            int MinValue = node.key;
            while(node.left != null)
            {
                MinValue = node.left.key;
                node = node.left;
            }

            return MinValue;
        }

        //Recorridos
        //izquierda, nodo, derecha
        public void inOrder()
        {
            inOrderRecursive(root);
        }

        private void inOrderRecursive(AVLNode node)
        {
            if(node != null)
            {
                inOrderRecursive(node.left);
                Console.Write(node.key + " ");
                inOrderRecursive(node.right);
            }
        }

        //nodo, izquierda, derecha
        public void preOrder()
        {
            preOrderRecursive(root);
        }

        private void preOrderRecursive(AVLNode node)
        {
            if(node != null)
            {
                Console.Write(node.key + " ");
                preOrderRecursive(node.left);
                preOrderRecursive(node.right);
            }
        }

        //izquierda, derecha, nodo
        public void postOrder()
        {
            postOrderRecursive(root);
        }

        private void postOrderRecursive(AVLNode node)
        {
            if(node != null)
            {
                postOrderRecursive(node.left);
                postOrderRecursive(node.right);
                Console.Write(node.key + " ");
            }
        }
    }
}
