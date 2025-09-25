'use client'
import { AiOutlinePlus } from 'react-icons/ai'
import Modal from './Modal'
import { FormEventHandler, useState } from 'react'
import { addTodo } from '@/app/api'
import { useRouter } from 'next/navigation'
import { v4 as uuidv4 } from 'uuid'

const AddTask = () => {
  const router = useRouter()
  const [modalOpen, setModalOpen] = useState<boolean>(false)
  const [newTaskValue, setNewTaskValue] = useState<string>('')
  //该函数在表单onsubmit时被调用
  const handleSumbitNewTodo: FormEventHandler<HTMLFormElement> = async (e) => {
    e.preventDefault() //阻止表单默认行为（阻止刷新页面)
    // addTodo 读取当前状态newTaskValue,传给API
    await addTodo({
      id: uuidv4(),
      text: newTaskValue,
    })
    //清空输入框
    setNewTaskValue('')
    setModalOpen(false)
    router.refresh()
  }
  return (
    <div>
      <button
        onClick={() => setModalOpen(true)}
        className="btn btn-primary w-full bg-pink-200 text-black"
      >
        Add New Task
        <AiOutlinePlus className="ml-2" size={20} />
      </button>
      <Modal modalOpen={modalOpen} setModalOpen={setModalOpen}>
        {/* 当用户点击add/回车时 会执行事件处理函数handlesubmitnewtodo 
        包括：1.防止表单刷新页面；2.读取当前输入newtaskvalue；3.调用addTodo把任务发给API；
        3.清空输入框*/}
        <form onSubmit={handleSumbitNewTodo}>
          <p className="font-bold text-lg">Add new task</p>
          {/* 把input放在modal-action 目的是输入框与按钮并排 */}
          <div className="modal-action">
            {/* onchange触发：输入框的值--> setNewTaskValue(新值)-->React 更新状态-->newTaskValue变新值 */}
            <input
              // input内为受控组件，输入框由react状态值来显示
              value={newTaskValue}
              //onchange 在每次用户输入/删除时触发，并把事件对象e传进来
              onChange={(e) => setNewTaskValue(e.target.value)} //获取当前输入框文本值 存入到react状态中
              type="text"
              placeholder="Type here"
              className="input input-border w-full"
            />
            <button type="submit" className="btn">
              Add
            </button>
          </div>
        </form>
      </Modal>
    </div>
  )
}

export default AddTask
