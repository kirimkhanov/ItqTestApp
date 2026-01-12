import { Outlet, useLocation } from 'react-router-dom';
import { useSelector } from 'react-redux';
import type { RootState } from '../store';

const ReferenceItemsLayout = () => {
  const location = useLocation();
  const isAddView = location.pathname.endsWith('/add');
  const totalCount = useSelector((state: RootState) => state.referenceItems.totalCount);

  return (
    <div className="app-container">
      <header className="app-header">
        <div className="container py-4">
          <div className="d-flex flex-wrap align-items-center justify-content-between gap-3">
            <div>
              <h1 className="display-6 fw-semibold mb-1">
                {isAddView ? 'Добавить значения' : 'Справочные значения'}
              </h1>
            </div>
          </div>
        </div>
      </header>

      <main className="container mt-3 pb-5">
        <Outlet />
      </main>
    </div>
  );
};

export default ReferenceItemsLayout;
