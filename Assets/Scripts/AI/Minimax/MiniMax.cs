using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace AI.MINIMAX
{

    public class Node<TEncounterState>
    {
        public TEncounterState State { get; set; } //The current state of the game (Player,AI,Ships,ETC)
        public LinkedList<Node<TEncounterState>> Children { get; set; } //List of possible future states
        public int Score { get; set; } //Score value for this node
        public bool IsMaximizingPlayer { get; set; } //Determines if this is AI or players turn

        public Node(TEncounterState state, bool isMaximizing)
        {
            State = state;
            Children = new LinkedList<Node<TEncounterState>>();
            IsMaximizingPlayer = isMaximizing;
            Score = 0;
        }

 

        public void AddChild(Node<TEncounterState> child)
        {
            Children.AddLast(child);
        }
    }
    
    public class Tree<TEncounterState>
    {
        public Node<TEncounterState> Root { get; private set; }

        public Tree(TEncounterState rootState, bool isMaximizing)
        {
            Root = new Node<TEncounterState>(rootState, isMaximizing);
        }

        public void GenerateTree(int depth,Func<TEncounterState,LinkedList<TEncounterState>> generateChildren,Node<TEncounterState> currentNode = null )
        {
            if (currentNode == null)
            {
                currentNode = Root;
            }
            if (depth == 0 )
            {
                return;
            }
            
            LinkedList<TEncounterState> childrenStates = generateChildren(currentNode.State);

            foreach (TEncounterState childState in childrenStates)
            {
                Node<TEncounterState> childNode = new Node<TEncounterState>(childState,!currentNode.IsMaximizingPlayer );
                currentNode.AddChild(childNode);
                GenerateTree(depth - 1, generateChildren, childNode);
            }
        }
        
        void EvaluateState()
        {
            
        }
        
        public void PrintTree(Node<TEncounterState> node, string indent = "", bool isLast = true)
        {
            
            EncounterState state =  node.State as EncounterState;
            // Print the current node
            Debug.Log(indent + (isLast ? "└─ " : "├─ ") + "Current Turn Ship: " + state.currentTurnShip+ ", Score: " + node.Score);

            indent += isLast ? "   " : "│  ";

            // Recursively print children nodes
            for (int i = 0; i < node.Children.Count; i++)
            {
                var childNode = node.Children.ElementAt(i);
                PrintTree(childNode, indent, i == node.Children.Count - 1);
            }
        }

        public void PrintTotalNumberOfChildren()
        {
            
        }
            
    }
}
