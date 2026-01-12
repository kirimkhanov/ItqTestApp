import type { ReferenceItem } from '../features/referenceItems/referenceItemsSlice';

export type FetchReferenceItemsResponse = {
  items: ReferenceItem[];
  totalCount: number;
};

type FetchReferenceItemsRawResponse = {
  items?: ReferenceItem[];
  Items?: ReferenceItem[];
  totalCount?: number;
  TotalCount?: number;
};

export const fetchReferenceItems = async (
  page: number,
  pageSize: number
): Promise<FetchReferenceItemsResponse> => {
  const response = await fetch(`/api/ReferenceItems?page=${page}&pageSize=${pageSize}`);
  if (!response.ok) {
    throw new Error('Failed to load reference items.');
  }

  const data = (await response.json()) as FetchReferenceItemsRawResponse;
  return {
    items: data.items ?? data.Items ?? [],
    totalCount: data.totalCount ?? data.TotalCount ?? 0,
  };
};

export const replaceReferenceItems = async (
  payload: Array<Record<string, string>>
): Promise<void> => {
  const response = await fetch('/api/ReferenceItems', {
    method: 'PUT',
    headers: {
      'Content-Type': 'application/json',
    },
    body: JSON.stringify(payload),
  });

  if (!response.ok) {
    throw new Error('Не удалось сохранить данные.');
  }
};
