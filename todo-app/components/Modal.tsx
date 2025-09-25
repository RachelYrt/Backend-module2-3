import React from 'react'
interface modalProps {
  modalOpen: boolean
  setModalOpen: (open: boolean) => boolean | void
  children: React.ReactNode
}
const Modal: React.FC<modalProps> = ({ modalOpen, setModalOpen, children }) => {
  return (
    // 等价与 classname="modal modalOpen" 打开弹窗 或 "modal" 隐藏弹窗
    <div className={`modal ${modalOpen ? 'modal-open' : ''}`}>
      <div className="modal-box">
        <label
          onClick={() => setModalOpen(false)}
          className="btn btn-sm btn-circle btn-ghost absolute right-2 top-2"
        >
          ✕
        </label>
        {children}
      </div>
    </div>
    // </dialog>
  )
}
export default Modal
