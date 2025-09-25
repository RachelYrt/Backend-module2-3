'use client'
import { AiOutlinePlusCircle } from 'react-icons/ai'
import { useRouter } from 'next/navigation'
import { Button } from '@/components/ui/button'

const AddTask = () => {
  const router = useRouter()
  return (
    <div>
      <Button
        onClick={() => router.push('/add-task')}
        className="btn btn-primary w-full bg-pink-100 border-blue-100 text-black
        hover:bg-blue-200 
        transition-all duration-300"
      >
        Add New Task
        <AiOutlinePlusCircle className="ml-2" size={30} />
      </Button>
    </div>
  )
}

export default AddTask
