'use client'
//父组件todoList接受task（任务数组）
import React, { useEffect } from 'react'
import Task from './Task'
import {
  Table,
  TableRow,
  TableHeader,
  TableHead,
  TableBody,
} from '@/components/ui/table'
import { useTodoStore } from '@/lib/store/todoStore'
//从store取tasks.map 渲染<task/>
//react组件 用react.FC声明它的prop类型 接受props返回JSX
export default function TodoList() {
  const tasks = useTodoStore((s) => s.tasks)
  const fetchTask = useTodoStore((s) => s.fetchTask)
  useEffect(() => {
    fetchTask()
  }, [fetchTask])
  if (!tasks.length) return <p>No tasks yet.</p>
  return (
    <div className="overflow-x-auto">
      <Table className="w-full gap-5">
        <TableHeader className="bg-gray-200">
          <TableRow>
            <TableHead className="w-full">Tasks</TableHead>
            <TableHead className="w-[150px] text-right">Action</TableHead>
          </TableRow>
        </TableHeader>
        <TableBody>
          {/* map()遍历tasks数组，每个task渲染一个Task组件 */}
          {tasks.map((task) => (
            <Task key={task.id} task={task} />
          ))}
        </TableBody>
      </Table>
    </div>
  )
}
