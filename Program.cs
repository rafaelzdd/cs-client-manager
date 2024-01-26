using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace Gestor_de_clientes
{
    internal class Program
    {
        [System.Serializable]
        struct Cliente
        {
            public string nome;
            public string email;
            public string cpf;
        }

        static List<Cliente> clientes = new List<Cliente>();
        enum Menu
        {
            Listar = 1, Adicionar, Remover, Sair
        }
        static void Main(string[] args)
        {
            Carregar();
            bool escolheuSair = false;

            while(!(escolheuSair))
            {

                Console.WriteLine("Sistema de clientes - Bem vindo!");
                Console.WriteLine("1- Listar\n2- Adicionar\n3- Remover\n4- Sair");
            
                int intOp = int.Parse(Console.ReadLine());

                Menu opcao = (Menu)intOp;

            
                switch(opcao)
                {
                    case Menu.Listar:
                    Listar();
                    break;

                    case Menu.Adicionar:
                    Adicionar();
                    break;

                    case Menu.Remover:
                    Remover();
                    break;

                    case Menu.Sair:
                    escolheuSair = true;
                    break;

                    default:
                    break;
                }
                Console.Clear();
            }

        }

        static void Adicionar()
        {
            Cliente cliente = new Cliente();
            Console.WriteLine("Cadastro de cliente\n=========");
            Console.WriteLine("Nome do cliente: ");
            cliente.nome = Console.ReadLine();
            Console.WriteLine("Email do cliente: ");
            cliente.email = Console.ReadLine();
            Console.WriteLine("CPF do cliente");
            cliente.cpf = Console.ReadLine();
            
            clientes.Add(cliente);
            Salvar();
            
            Console.WriteLine("Cadastro concluído, aperte Enter para retornar ao menu.");
            Console.ReadLine();
        }

        static void Remover()
        {
            Listar();
            Console.WriteLine("Digite o ID do cliente a ser removido:");
            int id = int.Parse(Console.ReadLine());
            if(id >= 0 && id < clientes.Count)
            {
                clientes.RemoveAt(id);
                Salvar();
            }
            else
            {
                Console.WriteLine("ID digitado é inválido, tente novamente.");
                Console.ReadLine();
            }
        }
        static void Listar()
        {
            if(clientes.Count > 0)
            {
                int i = 0;
                Console.WriteLine("Lista de clientes: ");
                foreach(Cliente cliente in clientes)
                {
                    Console.WriteLine($"ID: {i}");
                    Console.WriteLine($"Nome: {cliente.nome}");
                    Console.WriteLine($"E-mail: {cliente.email}");
                    Console.WriteLine($"CPF: {cliente.cpf}");
                    Console.WriteLine("================================");
                    i++;
                }
                Console.WriteLine("Aperte Enter para retornar ao menu.");
                Console.ReadLine();
            }
            else
            {
                Console.WriteLine("Nenhum cliente cadastrado. " +
                    "Cadastre um cliente. " +
                    "\nAperte Enter para sair.");
                Console.ReadLine();
            }

        }

        static void Salvar()
        {
            FileStream stream = new FileStream("clients.dat", FileMode.OpenOrCreate);
            BinaryFormatter encoder = new BinaryFormatter();

            encoder.Serialize(stream, clientes);

            stream.Close();
        }

        static void Carregar()
        {
            FileStream stream = new FileStream("clients.dat", FileMode.OpenOrCreate);
            try
            {
                BinaryFormatter encoder = new BinaryFormatter();

                clientes = (List<Cliente>)encoder.Deserialize(stream);

                if(clientes == null)
                {
                    clientes = new List<Cliente>();
                }

            }
            catch(Exception ex)
            {
                clientes = new List<Cliente>();
            }

            stream.Close();
        }

    }
}
