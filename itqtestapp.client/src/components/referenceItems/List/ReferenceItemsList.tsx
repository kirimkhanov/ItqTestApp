import type { ReferenceItem, ReferenceItemsState } from '../../../features/referenceItems/referenceItemsSlice';
import { usePagination } from '../../../hooks/usePagination';
import { Pagination } from '../../common';
import ReferenceItemsPageSizeSelect from '../PageSizeSelect';
import ReferenceItemsSearch from '../Search';

type Props = {
  items: ReferenceItem[];
  totalCount: number;
  page: number;
  pageSize: number;
  search: string;
  status: ReferenceItemsState['status'];
  error: ReferenceItemsState['error'];
  onPageChange: (nextPage: number) => void;
  onPageSizeChange: (pageSize: number) => void;
  onSearchChange: (value: string) => void;
  onAdd: () => void;
};

const ReferenceItemsList = ({
  items,
  totalCount,
  page,
  pageSize,
  search,
  status,
  error,
  onPageChange,
  onPageSizeChange,
  onSearchChange,
  onAdd,
}: Props) => {
  const { totalPages, visiblePages, startIndex, endIndex } = usePagination(
    totalCount,
    page,
    pageSize
  );

  return (
    <div className="card border-0 shadow-sm">
      <div className="card-body">
        <div className="d-flex flex-wrap align-items-center justify-content-between gap-3 mb-3">
          <ReferenceItemsPageSizeSelect
            value={pageSize}
            onChange={onPageSizeChange}
          />
          <div className="d-flex align-items-center gap-2">
            <ReferenceItemsSearch value={search} onChange={onSearchChange} />
            <button className="btn btn-primary btn-sm" type="button" onClick={onAdd}>
              Добавить
            </button>
          </div>
        </div>

        {status === 'loading' && (
          <div className="py-5 text-center">
            <div className="spinner-border text-primary" role="status" />
            <p className="text-muted mt-3 mb-0">Загружаем справочник...</p>
          </div>
        )}

        {status === 'failed' && (
          <div className="alert alert-danger mb-0" role="alert">
            {error}
          </div>
        )}

        {status !== 'loading' && status !== 'failed' && (
          <div className="table-responsive">
            <table className="table table-hover align-middle mb-0">
              <thead className="table-light">
                <tr>
                  <th scope="col">№</th>
                  <th scope="col">Код</th>
                  <th scope="col">Значение</th>
                </tr>
              </thead>
              <tbody>
                {items.length === 0 && (
                  <tr>
                    <td colSpan={3} className="text-center text-muted py-4">
                      Нет данных.
                    </td>
                  </tr>
                )}
                {items.map((item) => {
                  const code = item.code;
                  const value = item.value;
                  const rowNumber = item.rowNumber;

                  return (
                    <tr key={code ?? rowNumber}>
                      <td>{rowNumber}</td>
                      <td>{code}</td>
                      <td>{value}</td>
                    </tr>
                  );
                })}
              </tbody>
            </table>
          </div>
        )}

        <div className="d-flex flex-wrap align-items-center justify-content-between gap-3 mt-3">
          <span className="small text-muted">
            Показано {startIndex}-{endIndex} из {totalCount}
          </span>
          <Pagination
            page={page}
            totalPages={totalPages}
            visiblePages={visiblePages}
            onPageChange={onPageChange}
          />
        </div>
      </div>
    </div>
  );
};

export default ReferenceItemsList;







