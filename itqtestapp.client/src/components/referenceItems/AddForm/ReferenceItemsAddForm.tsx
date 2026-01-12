import { useState } from 'react';
import { replaceReferenceItems } from '../../../services/referenceItemsApi';

type FormRow = {
  code: string;
  value: string;
};

type SubmitState = {
  status: 'idle' | 'loading' | 'succeeded' | 'failed';
  error: string | null;
};

type Props = {
  onBack: () => void;
  onSaved: () => void;
};

const ReferenceItemsAddForm = ({ onBack, onSaved }: Props) => {
  const [rows, setRows] = useState<FormRow[]>([{ code: '', value: '' }]);
  const [submitState, setSubmitState] = useState<SubmitState>({
    status: 'idle',
    error: null,
  });

  const handleAddRow = () => {
    setRows((current) => [...current, { code: '', value: '' }]);
  };

  const handleRowChange = (index: number, field: keyof FormRow, value: string) => {
    setRows((current) =>
      current.map((row, rowIndex) =>
        rowIndex === index ? { ...row, [field]: value } : row
      )
    );
  };

  const handleResetForm = () => {
    setRows([{ code: '', value: '' }]);
    setSubmitState({ status: 'idle', error: null });
  };

  const handleSubmit = async (event: React.FormEvent<HTMLFormElement>) => {
    event.preventDefault();
    setSubmitState({ status: 'loading', error: null });

    const trimmedRows = rows.map((row) => ({
      code: row.code.toString().trim(),
      value: row.value.toString().trim(),
    }));

    if (trimmedRows.some((row) => row.code.length === 0 || row.value.length === 0)) {
      setSubmitState({
        status: 'failed',
        error: 'Заполните код и значение для всех строк.',
      });
      return;
    }

    const codes = trimmedRows.map((row) => Number(row.code));
    if (codes.some((code) => Number.isNaN(code))) {
      setSubmitState({ status: 'failed', error: 'Код должен быть целым числом.' });
      return;
    }

    const payload = trimmedRows.map((row) => ({ [row.code]: row.value }));

    try {
      await replaceReferenceItems(payload);

      setSubmitState({ status: 'succeeded', error: null });
      handleResetForm();
      onSaved();
    } catch (submitError) {
      const message =
        submitError instanceof Error
          ? submitError.message
          : 'Произошла ошибка при сохранении.';
      setSubmitState({ status: 'failed', error: message });
    }
  };

  return (
    <div className="card border-0 shadow-sm">
      <div className="card-body">
        <div className="d-flex flex-wrap align-items-center justify-content-between gap-3 mb-4">
          <div>
            <h2 className="h5 mb-1">Новые записи</h2>
            <p className="text-muted mb-0">
              Добавьте код и значение, чтобы заменить текущий набор данных.
            </p>
          </div>
          <button className="btn btn-outline-secondary btn-sm" type="button" onClick={onBack}>
            Вернуться к списку
          </button>
        </div>

        <form onSubmit={handleSubmit}>
          <div className="stacked-rows">
            <div className="row g-2 align-items-center mb-2 text-muted small">
              <div className="col-12 col-md-4">Код</div>
              <div className="col-12 col-md-6">Значение</div>
              <div className="col-12 col-md-2 text-md-end">Добавить</div>
            </div>
            {rows.map((row, index) => (
              <div key={`row-${index}`} className="row g-2 align-items-center mb-2">
                <div className="col-12 col-md-4">
                  <input
                    type="number"
                    className="form-control"
                    placeholder="Например, 101"
                    value={row.code}
                    onChange={(event) => handleRowChange(index, 'code', event.target.value)}
                    required
                  />
                </div>
                <div className="col-12 col-md-6">
                  <input
                    type="text"
                    className="form-control"
                    placeholder="Введите значение"
                    value={row.value}
                    onChange={(event) => handleRowChange(index, 'value', event.target.value)}
                    required
                  />
                </div>
                <div className="col-12 col-md-2 text-md-end">
                  <button
                    className="btn btn-light border"
                    type="button"
                    onClick={handleAddRow}
                    aria-label="Добавить строку"
                  >
                    +
                  </button>
                </div>
              </div>
            ))}
          </div>

          {submitState.status === 'failed' && (
            <div className="alert alert-danger mt-3" role="alert">
              {submitState.error}
            </div>
          )}

          <div className="d-flex flex-wrap align-items-center gap-2 mt-4">
            <button
              className="btn btn-primary"
              type="submit"
              disabled={submitState.status === 'loading'}
            >
              {submitState.status === 'loading' ? 'Сохраняю...' : 'Сохранить'}
            </button>
            <button
              className="btn btn-outline-secondary"
              type="button"
              onClick={handleResetForm}
              disabled={submitState.status === 'loading'}
            >
              Очистить
            </button>
          </div>
        </form>
      </div>
    </div>
  );
};

export default ReferenceItemsAddForm;


