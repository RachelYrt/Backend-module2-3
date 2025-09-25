import { ITask, NewTask } from '@/types/tasks'

const baseUrl = 'http://localhost:5126/Todo'
//promise<ITask[]>函数返回一个promise最终值是ITask[]类型(任务数组)
export const getallTodos = async (): Promise<ITask[]> => {
  const res = await fetch(`${baseUrl}`, { cache: 'no-store' })
  if (!res.ok) throw new Error('Failed to fetch todos')
  return res.json()
}
export const addTodo = async (todo: NewTask): Promise<ITask> => {
  const res = await fetch(baseUrl, {
    method: 'POST',
    headers: {
      'Content-Type': 'application/json',
    },
    body: JSON.stringify(todo),
  })
  // const newTodo = await res.json()
  if (!res.ok) throw new Error('Failed to add todo')
  return res.json()
}
export const editTodo = async (todo: ITask): Promise<ITask> => {
  const res = await fetch(`${baseUrl}/${todo.id}`, {
    method: 'PUT',
    headers: {
      'Content-Type': 'application/json',
    },
    body: JSON.stringify(todo),
  })
  // const updateTodo = await res.json()
  if (!res.ok) throw new Error('Failed to update todo')
  return res.json()
}
export const deleteTodo = async (id: string): Promise<void> => {
  const res = await fetch(`${baseUrl}/${id}`, {
    method: 'DELETE',
  })
  if (!res.ok) throw new Error('Failed to add todo')
}
