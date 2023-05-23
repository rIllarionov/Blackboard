
using System;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;

public class MouseDrawing : MonoBehaviour
{
    [SerializeField] private LineRenderer _linePrefab;//префаб линии, который мы будем модифицировать и создавать
    private Color? _lineColor; //сюда будем записывать цвет для присвоения линии

    private List<LineRenderer> _lineRenderers = new ();//массив для сохранения ссылок на созданные объекты
    private LineRenderer _line;//текущая линия которую рисуем
    
    private bool _isDrawing;//нужен для понимания рисуем ли мы в данный момент (нажата ли кнопка)


    private void Update()
    {
        if (_lineColor==null) //если мы еще не назначили цвет, значит рисовать не можем
        {
            return;
        }
        
        if (!_isDrawing && Input.GetMouseButtonDown(0))//если в данный момент не рисуем и нажали кнопку
        {
            StartDrawing();//то начинаем рисование и внутри меняем флаг на тру
        }
        
        if (_isDrawing)//если флаг выставлен в тру, то запускаем метод по отрисовке
        {
            Drawing();
        }
        
        if (_isDrawing && Input.GetMouseButtonUp(0))//если рисуем и подняли кнопку то прекращаем рисовать
        {
            StopDrawing();
        }
    }
    private void StartDrawing()
    {
        _line = Instantiate(_linePrefab);//создаем новую линию из префаба
        
        if (_lineColor!=null)//если цвет для нее был назначен,
        {
            _line.material.color = _lineColor.Value;//то присваиваем
        }
        
        _lineRenderers.Add(_line);//сразу добавляем новую линию в массив со всеми линиями
        _line.sortingOrder = _lineRenderers.Count;//сортируем их там чтобы каждая следующая линия шла поверх предыдущей
        _isDrawing = true;//запускаем метод рисования
    }
    private void Drawing()
    {
        
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition); //создаем луч

        if (Physics.Raycast(ray, out RaycastHit hit)) //попали лучем и забрали инфу о точке
        {
            var mousePosition = hit.point;//снимаем точку в которую попали

            Vector3 lastPossition;

            if (_line.positionCount>0)//если у нас уже были точки ранее, то
            {
                lastPossition = _line.GetPosition(_line.positionCount-1);//проверяем текущее расстояние
                                                                         //от предыдущей точки до текущей точки мышки
                
                if (Vector3.Distance(lastPossition,mousePosition)<0.1)//если это расстояние совсем маленькое,
                {
                    return;//то выходим чтобы не лепить новую точку в той же позиции объекта
                }
            }
            
            _line.positionCount ++; //добавляем кол-во точек в текущей линии
            _line.SetPosition(_line.positionCount-1,mousePosition);// иначе сетим новую точку в текущую точку мышки
        }
    }

    private void StopDrawing() 
    {
        if (_line.positionCount < 2) //если кол-во точек в линии меньше 2, то это был просто клик
        {
            Destroy(_line.gameObject); //удаляем данный объект 
            _lineRenderers.Remove(_line); //стираем из массива
        }
        
        _isDrawing = false;//останавливаем процесс рисования
    }



    public void ChangeColor(Color color)
    {
        _lineColor = color;
    }
    
    [UsedImplicitly]
    public void EraseAll()
    {
        foreach (var lines in _lineRenderers)
        {
            Destroy(lines.gameObject);
        }
        _lineRenderers = new List<LineRenderer>();
    }
    
}
