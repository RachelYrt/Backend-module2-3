'use client'
import { useEffect } from 'react'
import TodoList from './TodoList'
import { useTodoStore } from '@/lib/store/todoStore'
import AddTask from './AddTask'
//客户端容器，useEffect(fetchTask)渲染AddTask/Todolist

export default function TodoApp() {
  const fetchTask = useTodoStore((s) => s.fetchTask)
  const loading = useTodoStore((s) => s.loading)
  const error = useTodoStore((s) => s.error)
  // 三个selector分别订阅fetchTask,loading,error
  useEffect(() => {
    //useEffect 处理副作用 保证逻辑在渲染之后执行，fetchTask旨在初次渲染或fetchtask变更时执行
    fetchTask() //返回promise<void> 无需await，异常在action内部捕捉
  }, [fetchTask])
  //首屏拉取/灌入任务
  return (
    <main className="flex justify-center">
      <div className="w-full px-4 max-w-4xl flex flex-col gap-4">
        <h1 className="text-4xl font-bold text-center">Todo App</h1>
        <AddTask />
        {loading && <p>Loading...</p>}
        {error && <p className="text-red-500">{error}</p>}
        {/* 条件渲染，等同于if,调用set({loading:true})或set({error:""}) store改变，组件重新渲染 */}
        <TodoList />
      </div>
    </main>
  )
}
