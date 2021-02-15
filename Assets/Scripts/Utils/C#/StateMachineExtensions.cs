using Appccelerate.StateMachine.Syntax;
using System;

public static class StateMachineExtensions
{
    /// <summary>
    /// Defines an entry action.
    /// </summary>
    /// <param name="action">The action.</param>
    /// <returns>Entry action syntax.</returns>
    public static IEntryActionSyntax<TState, TEvent> OnEntry<TState, TEvent>(this IEntryActionSyntax<TState, TEvent> ie, Action action)
    {
        return ie.ExecuteOnEntry(action);
    }

    /// <summary>
    /// Defines an entry action.
    /// </summary>
    /// <param name="action">The action.</param>
    /// <returns>Entry action syntax.</returns>
    /// <typeparam name="T">Type of the event argument passed to the action.</typeparam>
    public static IEntryActionSyntax<TState, TEvent> OnEntry<TState, TEvent, T>(this IEntryActionSyntax<TState, TEvent> ie, Action<T> action)
    {
        return ie.ExecuteOnEntry<T>(action);
    }

    /// <summary>
    /// Defines an entry action.
    /// </summary>
    /// <typeparam name="T">Type of the parameter of the entry action method.</typeparam>
    /// <param name="action">The action.</param>
    /// <param name="parameter">The parameter that will be passed to the entry action.</param>
    /// <returns>Entry action syntax.</returns>
    public static IEntryActionSyntax<TState, TEvent> OnEntryParametrized<TState, TEvent, T>(this IEntryActionSyntax<TState, TEvent> ie, Action<T> action, T parameter)
    {
        return ie.ExecuteOnEntryParametrized(action, parameter);
    }

    /// <summary>
    /// Defines an exit action.
    /// </summary>
    /// <param name="action">The action.</param>
    /// <returns>Exit action syntax.</returns>
    public static IExitActionSyntax<TState, TEvent> OnExit<TState, TEvent>(this IExitActionSyntax<TState, TEvent> ie, Action action)
    {
        return ie.ExecuteOnExit(action);
    }

    /// <summary>
    /// Defines an exit action.
    /// </summary>
    /// <param name="action">The action.</param>
    /// <returns>Exit action syntax.</returns>
    /// <typeparam name="T">Type of the event argument passed to the action.</typeparam>
    public static IExitActionSyntax<TState, TEvent> OnExit<TState, TEvent, T>(this IExitActionSyntax<TState, TEvent> ie, Action<T> action)
    {
        return ie.ExecuteOnExit<T>(action);
    }

    /// <summary>
    /// Defines an exit action.
    /// </summary>
    /// <typeparam name="T">Type of the parameter of the exit action method.</typeparam>
    /// <param name="action">The action.</param>
    /// <param name="parameter">The parameter that will be passed to the Exit action.</param>
    /// <returns>Exit action syntax.</returns>
    public static IExitActionSyntax<TState, TEvent> OnExitParametrized<TState, TEvent, T>(this IExitActionSyntax<TState, TEvent> ie, Action<T> action, T parameter)
    {
        return ie.ExecuteOnExitParametrized(action, parameter);
    }
}