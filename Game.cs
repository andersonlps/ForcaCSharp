using System;
using System.Collections.Generic;

namespace ForcaCSharp
{
    // Representa o jogo
    class Game
    {
        /// <summary>
        /// Indica o número máximo de erros que o jogador pode ter antes do jogo terminar.
        /// </summary>
        private int maxErrors;

        /// <summary>
        /// Construtor.
        /// </summary>
        /// <param name="maxErrors">Número máximo de erros possíveis.</param>
        public Game(int maxErrors)
        {
            this.maxErrors = maxErrors;
        }

        /// <summary>
        /// Jogar o jogo.
        /// </summary>
        public void Play()
        {
            //Cria o objeto que vai gerenciar a palavra ativa do jogo.
            Word w = new Word();

            while (true)
            {
                Console.WriteLine("----- JOGO DA FORCA -----");
                Console.WriteLine("Você pode errar no máximo {0} vezes\n", maxErrors);

                //Variável para controlar os erros do jogador;
                int errors = 0;

                //Set para armazemar as letras já tentadas (para evitar que o jogador as tente novamente.)
                ISet<char> triedLetters = new HashSet<char>();

                //O jogo fica ativo enquanto a palavra não for encontrada por completo e enquanto o jogador
                //não atinge o número máximo de erros.
                while (!w.Finished && errors < maxErrors)
                {
                    Console.WriteLine(w.PartialWord);

                    //Solicita a letra ao jogador.
                    Console.WriteLine("\nDigite uma letra: ");
                    string letter = Console.ReadLine();

                    //Se o jogador não digita nada ele solicita novamente.
                    if (string.IsNullOrWhiteSpace(letter))
                    {
                        continue;
                    }
                    //Verifica se o jogador já não tentou essa letra.
                    if (triedLetters.Contains(letter[0]))
                    {
                        //Se já tentou solicita novamente.
                        Console.WriteLine("A letra {0} já foi tentada\n: ", letter[0]);
                        continue;
                    }
                    else
                    {
                        //Se não tentou, adiciona a letra no set de letras tentadas.
                        triedLetters.Add(letter[0]);
                    }
                    //Procura a letra na palavra.
                    bool found = w.Guess(letter[0]);

                    if(found)
                    {
                        //Se encontrou mostra a mensagem.
                        Console.WriteLine("Parabéns, a letra {0} foi encontrada!", letter[0]);
                    }
                    else
                    {
                        //Se não encontrou, incrementa o número de erros e mostra a mensagem.
                        errors++;
                        Console.WriteLine("Sinto muito, a letra {0} não existe na palavra, Você errou {1} vez(es).", letter[0], errors);
                    }

                    Console.WriteLine();
                }

                if (errors <  maxErrors)
                {
                    //Se o número máximo de erros não for atingido o jogador ganhou.
                    Console.WriteLine("\nVocê adivinhou a palavra: {0}. Deseja jogar mais uma vez? (S/N): ", w.CompleteWord);
                }
                else
                {
                    //Se o número máximo de erros for atingido o jogador perdeu.
                    Console.WriteLine("\nVocê não adivinhou a palavra que era: {0}. Deseja jogar mais uma vez? (S/N): ", w.CompleteWord);
                }

                //Aguarda a opção do jogador sobre jogar novamente.
                string playAgain = Console.ReadLine();

                if (playAgain.Length > 0 && (playAgain[0] == 's' || playAgain[0] == 'S'))
                {
                    //Se o usuário digitou 's' ou 'S', jogar novamente.
                    Console.WriteLine("OK, vamos jogar novamente.");

                    //Vai para a próxima palavra.
                    w.Next();

                    //Limpa o console.
                    Console.Clear();
                }
                else
                {
                    //Qualquer uma outra opção digitada termina o jogo.
                    Console.WriteLine("Tchaw!");
                    break;
                }

            }
        }
    }
}