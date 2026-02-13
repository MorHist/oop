using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace Lr1.Interpreter
{
    public class QueryParser
    {
        public IExpression Parse(string query)
        {
            if (string.IsNullOrWhiteSpace(query))
                throw new ArgumentException("Запрос не может быть пустым");

            var tokens = Tokenize(query);
            Debug.WriteLine("Токены: " + string.Join(" | ", tokens));

            return ParseExpression(tokens, 0, tokens.Count - 1);
        }

        private List<string> Tokenize(string query)
        {
            var tokens = new List<string>();
            var currentToken = new StringBuilder();
            bool inQuotes = false;

            foreach (char c in query)
            {
                if (c == '\'' || c == '"')
                {
                    inQuotes = !inQuotes;
                    currentToken.Append(c);
                }
                else if ((c == ' ' || c == '(' || c == ')') && !inQuotes)
                {
                    if (currentToken.Length > 0)
                    {
                        tokens.Add(currentToken.ToString());
                        currentToken.Clear();
                    }
                    if (c == '(' || c == ')')
                        tokens.Add(c.ToString());
                }
                else
                {
                    currentToken.Append(c);
                }
            }

            if (currentToken.Length > 0)
                tokens.Add(currentToken.ToString());

            return tokens;
        }

        private IExpression ParseExpression(List<string> tokens, int start, int end)
        {
            if (start > end)
                throw new ArgumentException("Пустое выражение");

            // 1. Ищем OR на нулевом уровне вложенности
            int parenthesisLevel = 0;
            for (int i = start; i <= end; i++)
            {
                if (tokens[i] == "(")
                    parenthesisLevel++;
                else if (tokens[i] == ")")
                    parenthesisLevel--;
                else if (parenthesisLevel == 0 && tokens[i].ToUpper() == "OR")
                {
                    var left = ParseExpression(tokens, start, i - 1);
                    var right = ParseExpression(tokens, i + 1, end);
                    return new OrExpression(left, right);
                }
            }

            // 2. Ищем AND на нулевом уровне
            parenthesisLevel = 0;
            for (int i = start; i <= end; i++)
            {
                if (tokens[i] == "(")
                    parenthesisLevel++;
                else if (tokens[i] == ")")
                    parenthesisLevel--;
                else if (parenthesisLevel == 0 && tokens[i].ToUpper() == "AND")
                {
                    var left = ParseExpression(tokens, start, i - 1);
                    var right = ParseExpression(tokens, i + 1, end);
                    return new AndExpression(left, right);
                }
            }

            // 3. Если весь диапазон обёрнут в скобки — заходим внутрь
            if (tokens[start] == "(" && tokens[end] == ")")
            {
                // Проверим, что скобки парные (можно опустить для простоты)
                return ParseExpression(tokens, start + 1, end - 1);
            }

            // 4. Иначе — простое условие
            return ParseSimpleCondition(tokens, start, end);
        }

        private IExpression ParseSimpleCondition(List<string> tokens, int start, int end)
        {
            if (end - start != 2)
                throw new ArgumentException($"Неверное количество токенов в условии: {string.Join(" ", tokens.GetRange(start, end - start + 1))}");

            string field = tokens[start];
            string op = tokens[start + 1];
            string value = tokens[end];

            // Убираем кавычки
            if ((value.StartsWith("'") && value.EndsWith("'")) ||
                (value.StartsWith("\"") && value.EndsWith("\"")))
                value = value.Substring(1, value.Length - 2);

            switch (op.ToUpper())
            {
                case ">": return new GreaterThanExpression(field, value);
                case "<": return new LessThanExpression(field, value);
                case "=":
                case "==": return new EqualsExpression(field, value);
                case "CONTAINS": return new ContainsExpression(field, value);
                default:
                    throw new ArgumentException($"Неизвестный оператор '{op}' в условии: {string.Join(" ", tokens.GetRange(start, end - start + 1))}");
            }
        }
    }
}