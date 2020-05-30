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