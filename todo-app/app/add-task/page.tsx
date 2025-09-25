'use client'
import { useRouter } from 'next/navigation'
import React from 'react'
// import { addTodo } from '../api'
// import { FormEventHandler } from 'react'
import { Input } from '@/components/ui/input'
import { useForm } from 'react-hook-form'
import { useTodoStore } from '../../lib/store/todoStore'
// import { Textarea } from '@/components/ui/textarea'

type FormValues = {
  newTaskText: string
  description: string
}

export default function AddTaskPage() {
  const router = useRouter()
  const createTask = useTodoStore((s) => s.createTask)
  //useForm 创建表单实例，defaultvalues:初始值(等同于usestate(‘’))
  //register 输入控件注册给RHF; handlesubmit：包装提交函数、自动防默认、先校验
  //formState：表单状态（errors/isSubmitting)
  const {
    register,
    handleSubmit,
    reset,
    formState: { errors, isSubmitting },
  } = useForm<FormValues>({
    defaultValues: { newTaskText: '', description: '' },
  })
  //defaultValues 表单初始值
  //解构赋值 从类型是formValues的newtasktext的对象中取出该字段
  const onSubmit = async ({ newTaskText, description }: FormValues) => {
    await createTask(newTaskText, (description ?? '').trim())
    reset()
    router.push('/') //返回主页
  }

  return (
    <div className="max-w-xl mx-auto mt-8">
      <p className="font-bold text-lg">Add new task</p>
      <form
        //handlesubmit是RHF提供的包装器，阻止表单默认行为，校验通过后将字段值打包给 submit(newtasktext)
        onSubmit={handleSubmit(onSubmit)}
        className="flex flex-col items-center space-y-4"
      >
        <Input
          placeholder="type new task here..."
          //register('输入框字段名',校验规则rules)
          {...register('newTaskText', {
            required: 'Task is required',
            minLength: { value: 2, message: 'Text is Too short' },
          })}
          className="input input-border border-2 w-full
          bg-pink-100 text-black placeholder:text-gray-400 border-black-500
          rounded-md focus:border-pink-500 focus:ring-2 focus:ring-pink-500
           shadow-[0_0_10px_#3b82f6] focus:shadow-[0_0_20px_#ff00ff]  transition-all duration-300"
        />
        <textarea
          placeholder="describe this task"
          className="input  w-md
          bg-pink-100 text-black placeholder:text-gray-400 border-black-500"
          {...register('description', {
            required: '',
          })}
        ></textarea>
        {errors.newTaskText && (
          <span className="text-sm text-red-500">
            {errors.newTaskText.message}
          </span>
        )}
        <div>
          <button
            className="btn btn-primary w-32"
            //submit触发onsubmit回调
            type="submit"
            disabled={isSubmitting}
          >
            {/* 提交中防重复点击 */}
            {isSubmitting ? 'Adding...' : 'Add'}
          </button>
        </div>
      </form>
    </div>
  )
}
