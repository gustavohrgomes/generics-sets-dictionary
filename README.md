# Generics, Set e Dictionary

#### Generics

Os `Generics` permitem que **Classes**, **Interfaces** e **métodos** possam ser parametrizados por tipo. Seus benefícios são:

- Reuso
- Type Safaty
- Performance

O seu uso é muito comum em coleções, como por exemplo 

```csharp
List<string> list = new List<string>();
list.Add("Maria");
string name = list[0];
```

#### Restrições para generics (Type Safety)

Restrições informam o compilador sobre as funcionalidades que um argumento de tipo deve ter. Sem nenhuma restrição, o argumento de tipo poderia ser qualquer tipo. O compilador pode assumir somente os membros de `System.Object`, que é a classe base definitiva para qualquer tipo .NET.

Restrições são especificadas usando a palavra-chave contextual `where`. A tabela a seguir lista os sete tipos de restrições:

- where T : struct
- where T : class
- where T : unmanaged
- where T : new()
- where T : <base type name>
- where T : U

#### GetHashCode e Equals

São operações da classe Object utilizada para comparar se um objeto é igual a outro.

**Equals**: Lento, porém possui uma resposta de 100%
**GetHashCode**: Rápido, porém a resposta positiva não é 100%

Os tipos pré-definidos já possuem implementação para operações. Classes e structs personalizados precisam sobrepô-las.

#### Equals

É um método que compara se o objeto é igual a outro, retornando true ou false.

```csharp
string a = "Maria";
string b = "Alex";

Console.WriteLine(a.Equals(b));
```

#### GetHashCode

É um método que retorna um número inteiro representando um código gerado a partir dass informações do objeto

```csharp
string a = "Maria";
string b = "Alex";

Console.WriteLine(a.GetHashCode());
Console.WriteLine(b.GetHashCode());
```

#### Regra de ouro do GetHashCode

Se o código de dois objetos for diferente, então os dois objetos são diferentes

Se o código de dois objetos for igual, **muito provavelmente** os objetos são iguais (pode haver colisão)

#### GetHashCode e Equals Personalizados

```csharp
class Client {

  public string Name { get; set; }
  public string Email { get; set; }

  // Equals Personalizado
  public override bool Equals(Object obj) {
    
    if (!(obj is Client)) {
      return false;
    }

    Client other = obj as Client;    

    return Email.Equals(other.Email);
  }

  // GetHashCode Personalizado
  public override int GetHashCode() {
    return Email.GetHashCode();
  }
}
```

```csharp
class Program {

  static void Main(string[] args) {
    
    Client a = new Client { Name = "Maria", Email = "maria@gmail.com" };
    Client b = new Client { Name = "Alex", Email = "maria@gmail.com" }; // Email = "alex@gmail.com"

    Console.WriteLine(a.Equal(b));
    Console.WriteLine(a == b); // O operador de comparação == compara a referência do ponteiro de memória do objeto, diferente do método Equals
    Console.WriteLine(a.GetHashCode());
    Console.WriteLine(b.GetHashCode());

    // OUTPUT:

    // True
    // False 
    // 102453675
    // -123542262
  }
}
```

#### HashSet<T> e SortedSet<T>

Esses dois representam um conjunto de elementos (Similar ao da Álgebra):
  - Não admite repetições
  - Elementos não possuem posição
  - O acesso, inserção e remoção de elementos são rápidos
  - Oferece operações eficientes de conjuntos: interseção, união, diferença;

#### Diferenças

HashSet
  - Armazenamento em uma Hash Table
  - Extremamente rápido: inserção, remoção e busca O(1) (Ordem de 1)
  - A ordem dos elementos não é garantida

SortedSet
  - Armazenamento em árvore
  - Rápido: Inserção, remoção e busca O(log(n)) (busca logarítmica)
  - Os elementos são armazenados ordenamanente conforme implementação `IComparer<T>`


Alguns métodos importantes
  - Add
  - Clear
  - Contains
  - UnionWith(other) - operação união: adiciona no conjunto os elementos do outro conjunto, sem repetição
  - IntersectWith(other) - operação interseção: remove do conjunto os elementos não contidos em other
  - ExceptWith(other) - operação diferença: remove do conjuntos os elementos contidos em other
  - Remove(T)
  - RemoveWhere(predicate)

```csharp
using System;
using System.Collections.Generic;

namespace Course {

  class Program {
    static void Main(string[] args) {

      HashSet<string> set = new HashSet<string>();

      set.Add("TV");
      set.Add("Notebook");
      set.Add("Tablet");

      Console.WriteLine(set.Contains("Notebook"));

      foreach (String p in set) {
        Console.WriteLine(p);
      }
    }
  }
}
```

```csharp
using System;
using System.Collections.Generic;

namespace Course {
  class Program {
    static void Main(string[] args) {
      
      SortedSet<int> a = new SortedSet<int>() { 0, 2, 4, 5, 6, 8, 10 };
      SortedSet<int> b = new SortedSet<int>() { 5, 6, 7, 8, 9, 10 };
      
      //union
      SortedSet<int> c = new SortedSet<int>(a);
      c.UnionWith(b);
      printCollection(c);
      
      //intersection
      SortedSet<int> d = new SortedSet<int>(a);
      d.IntersectWith(b);
      printCollection(d);
      
      //difference
      SortedSet<int> e = new SortedSet<int>(a);
      e.ExceptWith(b);
      printCollection(e);
      }
      
      static void printCollection<T>(IEnumerable<T> collection) {
        
        foreach(T obj in collection) {
          Console.Write(obj + " ");
        }
        Console.WriteLine();
      }
  }
}
```

#### Como as coleções Hash testam igualdade?

Se `GetHashCode` e `Equals` estiverem implementados:
  - Primeiro GetHashCode. Se der igual, usa Equals para confirmar.

Se `GetHashCode` e `Equals` *NÃO* estiverem implementados:
  - Tipos referência: compara as refências dos objetos
  - Tipos valor: comparar os valores dos atributos

```csharp
namespace Course.Entities {
  struct Point {

  public int X { get; set; }
  public int Y { get; set; }

    public Point(int x, int y) : this() {
      X = x;
      Y = y;
    }
  }
}
```

```csharp
namespace Course.Entities {
  class Product {

  public string Name { get; set; }
  public double Price { get; set; }

  public Product(string name, double price) {
    Name = name;
    Price = price;
  }
}
```

```csharp
using System;
using System.Collections.Generic;
using Course.Entities;

namespace Course {
  class Program {
    static void Main(string[] args) {

      HashSet<Product> a = new HashSet<Product>();

      a.Add(new Product("TV", 900.0));
      a.Add(new Product("Notebook", 1200.0));

      HashSet<Point> b = new HashSet<Point>();

      b.Add(new Point(3, 4));
      b.Add(new Point(5, 10));
      
      Product prod = new Product("Notebook", 1200.0);
      Console.WriteLine(a.Contains(prod));
      Point point = new Point(5, 10);
      Console.WriteLine(b.Contains(point));
    }
  }
}
```