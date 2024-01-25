
import { ToastContainer, toast } from 'react-toastify';
import "react-toastify/dist/ReactToastify.css";




const Notify = (type,msg) => {
    switch (type) {

        case type == "warning":
            toast.warning(toast.warning(msg, { autoClose: 10000 }));
            break;
        case type == "success":
            toast.success(toast.success(msg, { autoClose: 10000 }));
            break;


        default:
            toast.warning(toast.warning(msg, { autoClose: 10000 }));
            break;
    }
}

export default Notify;