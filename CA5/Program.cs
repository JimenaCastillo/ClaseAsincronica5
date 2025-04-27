using System;

namespace CA5
{
    class Program
    {
        static void Main(string[] args)
        {
            AVLTree tree = new AVLTree();
            int opcion = 0;

            do
            {
                Console.WriteLine("\n--- MENU AVL TREE ---");
                Console.WriteLine("1. Insertar nodo");
                Console.WriteLine("2. Eliminar nodo");
                Console.WriteLine("3. Mostrar recorrido InOrder");
                Console.WriteLine("4. Mostrar recorrido PreOrder");
                Console.WriteLine("5. Mostrar recorrido PostOrder");
                Console.WriteLine("6. Salir");
                Console.Write("Selecciona una opción: ");

                if (int.TryParse(Console.ReadLine(), out opcion))
                {
                    switch (opcion)
                    {
                        case 1:
                            Console.Write("Ingresa el valor a insertar: ");
                            if (int.TryParse(Console.ReadLine(), out int valorInsertar))
                            {
                                tree.insert(valorInsertar);
                                Console.WriteLine($"Nodo {valorInsertar} insertado correctamente.");
                            }
                            else
                            {
                                Console.WriteLine("Entrada inválida.");
                            }
                            break;

                        case 2:
                            Console.Write("Ingresa el valor a eliminar: ");
                            if (int.TryParse(Console.ReadLine(), out int valorEliminar))
                            {
                                tree.delete(valorEliminar);
                                Console.WriteLine($"Nodo {valorEliminar} eliminado (si existía).");
                            }
                            else
                            {
                                Console.WriteLine("Entrada inválida.");
                            }
                            break;

                        case 3:
                            Console.WriteLine("\nRecorrido InOrder:");
                            tree.inOrder();
                            Console.WriteLine();
                            break;

                        case 4:
                            Console.WriteLine("\nRecorrido PreOrder:");
                            tree.preOrder();
                            Console.WriteLine();
                            break;

                        case 5:
                            Console.WriteLine("\nRecorrido PostOrder:");
                            tree.postOrder();
                            Console.WriteLine();
                            break;

                        case 6:
                            Console.WriteLine("¡Hasta luego!");
                            break;

                        default:
                            Console.WriteLine("Opción no válida. Intenta de nuevo.");
                            break;
                    }
                }
                else
                {
                    Console.WriteLine("Entrada inválida. Debes ingresar un número.");
                }

            } while (opcion != 6);
        }
    }
}
