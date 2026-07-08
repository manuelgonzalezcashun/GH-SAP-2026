using System.Collections.Generic;
using Ink.Runtime;

public class DialogueVariableObserver
{
    private Dictionary<string, Object> variables = new Dictionary<string, Object>();
    public Dictionary<string, Object> Variables => variables;
    public void StartListening(Story story)
    {
        VariablesToStory(story);
        story.variablesState.variableChangedEvent += VariableChanged;
    }
    public void StopListening(Story story)
    {
        story.variablesState.variableChangedEvent -= VariableChanged;
    }
    private void VariableChanged(string var_name, Object value)
    {
        if (!variables.ContainsKey(var_name))
            variables.Add(var_name, value);

        variables[var_name] = value;
    }
    private void VariablesToStory(Story story)
    {
        foreach (KeyValuePair<string, Object> variable in variables)
        {
            story.variablesState.SetGlobal(variable.Key, variable.Value);
        }
    }
}
