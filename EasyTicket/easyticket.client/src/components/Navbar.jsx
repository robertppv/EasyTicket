import { Link, useNavigate } from 'react-router-dom';

function Navbar({ user, setUser }) {
  const navigate = useNavigate();
  const handleButton = () => {
    const token = localStorage.getItem('token');
    if (token) {
      localStorage.clear('token');
      setUser(null);
      navigate('/');
    }
  };

  return (
    <nav className='mt-2 shadow-2xl bg-[#819067]  p-3  rounded-md mx-10 border-2 border-[#B1AB86]'>
      <div className='flex '>
        <Link to='/' className='text-white text-lg font-bold'>
          EasyTicket
        </Link>

        {user ? (
          <div className='flex ml-auto justify-center items-center '>
            <div className='  text-white text-lg  px-1 font-bold '>
              {user.name}
            </div>
            <button
              onClick={handleButton}
              className=' ml-5
               text-white bg-red-500 rounded px-2 py-1 hover:bg-red-600 font-bold pointer:'
            >
              Logout
            </button>
          </div>
        ) : (
          <div className='flex ml-auto'>
            <Link
              className='text-white mx-2 hover:underline font-bold'
              to='/login'
            >
              Login
            </Link>
            <Link
              className='text-white mx-2 hover:underline font-bold '
              to='/register'
            >
              Register
            </Link>
          </div>
        )}
      </div>
    </nav>
  );
}

export default Navbar;
