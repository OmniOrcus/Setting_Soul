using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Keep thi for future use!!!

public interface IObserver
{
    void Observe();

}

public abstract class ObservableBehaviour : MonoBehaviour
{

    List<IObserver> observers = new List<IObserver>();

    public void RegisterObserver(IObserver observer) {
        observers.Add(observer);
    }

    public void UnregisterObserver(IObserver observer)
    {
        observers.Remove(observer); ;
    }

    protected void InformObservers() {

        foreach (IObserver observer in observers) {
            observer.Observe();
        }
    }

}
